using System.Collections;
using System.Collections.Generic;
using Data.Util;
using UnityEngine;
using UnityEngine.UI;

public class Labs : Upgradable
{
    [Header("Lab Quests")]
    [SerializeField] private List<QuestInfo> labQuestInfos;
    [SerializeField] private VerticalLayoutGroup labQuestList;
    [SerializeField] private QuestButton questButton;

    public new void Start()
    {
        base.Start();
        
        labQuestInfos.ForEach(info =>
        {
            var quest = Instantiate(questButton, labQuestList.transform);
            quest.Initialize(info);
        });
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
            //Apply game world modifications
        }
    }

    public void TakeQuest(QuestInfo info)
    {
        switch (info.questNumber)
        {
            
            default: break;
        }
    }
}
