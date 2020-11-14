using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance;
    public static GameLogic Instance => _instance;

    public TurretControl Turret => turretControl;

    public MineHarvest MineControl => mineHarvest;

    public Hangar HangarControl => hangar;

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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        _instance = this;
    }

    private void Start()
    {
        IncrementAsteroids(0);
        IncrementMinerals(0);
        _clickEnable = true;
    }
    
    [Header("Game Properties")]
    [SerializeField] private int asteroidsCollected = 0;
    [SerializeField] private int mineralsCollected = 0;

    [Space(5)]
    
    [Header("Game Behaviours")]
    [SerializeField] private ResourcePanel resourcePanel;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TurretControl turretControl;
    [SerializeField] private MineHarvest mineHarvest;
    [SerializeField] private Hangar hangar;
    //Add Labs

    private bool _clickEnable;
    
    public void ToggleAsteroidSpawner(bool toggle)
    {
        asteroidSpawner.ToggleSpawner(toggle);
    }
    
    public void AsteroidDestroyed()
    {
        IncrementAsteroids(1);
    }

    public bool CheckFunds(int asteroidCost, int mineralCost)
    {
        return AsteroidsCollected >= asteroidCost && MineralsCollected >= mineralCost;
    }

    public void PayForUpgrade(int asteroidCost, int mineralCost)
    {
        if (CheckFunds(asteroidCost, mineralCost))
        {
            AsteroidsCollected -= asteroidCost;
            MineralsCollected -= mineralCost;
        }
    }
    
    public void IncrementAsteroids(int value)
    {
        AsteroidsCollected += value;
    }
    
    public void IncrementMinerals(int value)
    {
        MineralsCollected += value;
    }
}
