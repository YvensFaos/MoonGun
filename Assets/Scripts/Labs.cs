using UnityEngine;
using UnityEngine.UI;

public class Labs : Upgradable
{
    [Header("Lab Quests")] 
    [SerializeField] private QuestList initialQuest;
    [SerializeField] private VerticalLayoutGroup labQuestList;
    [SerializeField] private QuestButton questButton;
    
    [Header("Lab Canvas")]
    [SerializeField] private GameObject labCanvas;
    [SerializeField] private GameObject blockCanvas;

    public new void Start()
    {
        base.Start();
        
        initialQuest.quests.ForEach(info => { AddQuestToList(info); });
    }

    public void UnlockUpgrade(UpgradeInfo info)
    {
        if (info.name.Equals("Laser Shot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UnlockLaser();
            GameLogic.Instance.DisplayWeaponPanel();
        } else if (info.name.Equals("Mineral Laser"))
        {
            CloseLabCanvas();
            GameLogic.Instance.Cutscenes.TriggerMineralLaserCutscene();
            
        } else if (info.name.Equals("Break the Surface"))
        {
            CloseLabCanvas();
            GameLogic.Instance.Cutscenes.TriggerBreakTheSurfaceCutscene();
        }
    }

    public void TakeQuest(QuestInfo info)
    {
        GameLogic.Instance.QuestControl.ActivateQuest(info);
    }
    
    public void AddQuestToList(QuestInfo info)
    {
        var quest = Instantiate(questButton, labQuestList.transform);
        quest.Initialize(info);
    }

    private void CloseLabCanvas()
    {
        labCanvas.SetActive(false);
        blockCanvas.SetActive(false);
    }
}
