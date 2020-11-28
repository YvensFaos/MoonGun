using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance;
    public static GameLogic Instance => _instance;

    public TurretControl Turret => turretControl;

    public MineHarvest MineControl => mineHarvest;

    public Hangar HangarControl => hangar;

    public Labs LabsControl => labs;

    public QuestObserver QuestControl => questObserver;

    public AsteroidTreasure TreasureControl => treasurer;

    public CutsceneCommander Cutscenes => commander;

    public ShieldControl Shield => shield;
    
    public int AsteroidsCollected
    {
        get => asteroidsCollected;
        set
        {
            asteroidsCollected = value;
            resourcePanel.UpdateAsteroidText(AsteroidsCollected);
        }
    }

    public int MineralsCollected
    {
        get => mineralsCollected;
        set
        {
            mineralsCollected = value;
            resourcePanel.UpdateMineralText(MineralsCollected);
        }
    }

    public bool ClickEnable
    {
        get => _clickEnable;
        set => _clickEnable = value;
    }
    
    [Header("Game Properties")]
    [SerializeField] private int asteroidsCollected = 0;
    [SerializeField] private int mineralsCollected = 0;
    [SerializeField] private int shieldRestorationCost = 10;
    private float _restorationCostIncreaseFactor = 1.618f;

    public int ShieldRestorationCost => shieldRestorationCost; 
    
    [SerializeField, Range(0.0f, 1.0f)] 
    private float asteroidLightIntensity = 1.0f;

    public float LightIntensity => asteroidLightIntensity;

    [Space(5)]
    
    [Header("Game Behaviours")]
    [SerializeField] private ResourcePanel resourcePanel;
    [SerializeField] private WeaponTextPanel weaponTextPanel;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TurretControl turretControl;
    [SerializeField] private MineHarvest mineHarvest;
    [SerializeField] private Hangar hangar;
    [SerializeField] private Labs labs;
    [SerializeField] private Animator questCompletedAnimator;
    [SerializeField] private QuestObserver questObserver;
    [SerializeField] private PlaySound uiSound;
    [SerializeField] private AsteroidTreasure treasurer;
    [SerializeField] private CutsceneCommander commander;
    [SerializeField] private ShieldControl shield;
    
    [Header("Camera Elements")]
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    private CinemachineBasicMultiChannelPerlin _fightPerlin;

    private bool _clickEnable;

    [Header("Cheats")] 
    [SerializeField] private bool noFundsCheck;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        _instance = this;
        
        //Disable all cheats in the final build.
        if (!Application.isEditor)
        {
            noFundsCheck = false;    
        }
    }

    private void Start()
    {
        IncrementAsteroids(0);
        IncrementMinerals(0);
        _clickEnable = true;

        _fightPerlin = fightCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    public void ToggleAsteroidSpawner(bool toggle)
    {
        asteroidSpawner.ToggleSpawner(toggle);
    }
    
    public void AsteroidDestroyed(AsteroidType type = AsteroidType.REGULAR, int asteroids = 1, int minerals = 0)
    {
        IncrementAsteroids(asteroids);
        IncrementMinerals(minerals);
    }

    public void AsteroidEffect(AsteroidEffects effect)
    {
        switch (effect)
        {
            case AsteroidEffects.TREASURE:
                treasurer.GenerateTreasureFromAsteroid();
            break;
        }
    }

    public void DamageShield(AsteroidDestruction asteroid)
    {
        shield.TakeDamage(asteroid.DamagePower);
    }

    public bool CheckFunds(int asteroidCost, int mineralCost)
    {
        return noFundsCheck || (AsteroidsCollected >= asteroidCost && MineralsCollected >= mineralCost);
    }

    public void PayForUpgrade(int asteroidCost, int mineralCost)
    {
        if (CheckFunds(asteroidCost, mineralCost))
        {
            AsteroidsCollected = Mathf.Max(AsteroidsCollected - asteroidCost, 0);
            MineralsCollected =  Mathf.Max(MineralsCollected - mineralCost, 0);
        }
    }

    public void NotifyUpgradeForProgress(UpgradeInfo upgradeInfo)
    {
        var upgradeInfoName = upgradeInfo.name;
        LabsControl.CheckProgression(upgradeInfoName);
        HangarControl.CheckProgression(upgradeInfoName);
        MineControl.CheckProgression(upgradeInfoName);
    }
    
    public void IncrementAsteroids(int value)
    {
        AsteroidsCollected += value;
    }
    
    public void IncrementMinerals(int value)
    {
        MineralsCollected += value;
    }

    public void ShakeFightCamera(float intensity, float time)
    {
        _fightPerlin.m_FrequencyGain = intensity;
        DOTween.To(() => _fightPerlin.m_FrequencyGain,
            value => _fightPerlin.m_FrequencyGain = value,
            0.0f, time);
    }

    public void DisplayWeaponPanel()
    {
        weaponTextPanel.gameObject.SetActive(true);
    }

    public void ChangeWeapon(TurretCannonType type)
    {
        weaponTextPanel.ChangeTextTo(type);
    }

    public void QuestCompleted(QuestInfo questInfo)
    {
        questCompletedAnimator.SetTrigger("QuestCompleted");
        IncrementAsteroids(questInfo.asteroidReward);
        IncrementMinerals(questInfo.mineralReward);
    }

    public void ChangeAsteroidLightIntensity(float intensity)
    {
        asteroidLightIntensity = Mathf.Clamp01(intensity);
    }

    public void IncreaseShieldRestorationCost()
    {
        shieldRestorationCost = (int) (shieldRestorationCost * _restorationCostIncreaseFactor);
    }

    public void PlayUISound(AudioClip sound)
    {
        uiSound.Play(sound);
    }
}
