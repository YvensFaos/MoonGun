﻿using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
   [SerializeField] private Text asteroidCost;
   [SerializeField] private Text mineralCost;
   [SerializeField] private Text upgradeName;
   [SerializeField] private Text upgradeDescription;
   [SerializeField] private AudioClip upgradeSuccessSound;
   [SerializeField] private AudioClip upgradeFailedSound;
   
   [SerializeField] private FacilityType facility;

   private UpgradeInfo _upgradeInfo;
   
   public void Initialize(UpgradeInfo upgradeInfo, FacilityType facilityType)
   {
      facility = facilityType;
      _upgradeInfo = upgradeInfo;
      asteroidCost.text = upgradeInfo.asteroidCost.ToString();
      mineralCost.text = upgradeInfo.mineralCost.ToString();
      upgradeName.text = upgradeInfo.name;
      upgradeDescription.text = upgradeInfo.description;
   }

   public void ClickMe()
   {
      if (GameLogic.Instance.CheckFunds(_upgradeInfo.asteroidCost, _upgradeInfo.mineralCost))
      {
         GameLogic.Instance.PayForUpgrade(_upgradeInfo.asteroidCost, _upgradeInfo.mineralCost);
         GameLogic.Instance.PlayUISound(upgradeSuccessSound);
         switch (facility)
         {
            case FacilityType.LABS:
            {
               GameLogic.Instance.LabsControl.UnlockUpgrade(_upgradeInfo);
            }
               break;
            case FacilityType.MINES:
            {
               GameLogic.Instance.MineControl.UnlockUpgrade(_upgradeInfo);
            }
               break;
            case FacilityType.HANGAR:
            {
               GameLogic.Instance.HangarControl.UnlockUpgrade(_upgradeInfo);
            }
               break;
         }
         GameLogic.Instance.NotifyUpgradeForProgress(_upgradeInfo);
         Destroy(gameObject);
      }
      else
      {
         GameLogic.Instance.PlayUISound(upgradeFailedSound);
      }
   }
}
