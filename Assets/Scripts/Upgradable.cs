    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Upgradable : MonoBehaviour
    {
        [Header("Facility Identifier")] 
        [SerializeField] private FacilityType type;
        
        [Header("Upgrades")]
        [SerializeField] private UpgradeList initialUpgrades;
        [SerializeField] private VerticalLayoutGroup upgradesVerticalLayoutGroup;
        [SerializeField] private UpgradeButton upgradeButtonPrefab;
        [SerializeField] private List<UpgradeList> progressionUpgrades;

        public void Start()
        {
            AddUpgrades(initialUpgrades);
        }

        private void AddUpgradeToList(UpgradeInfo info)
        {
            var upgrade = Instantiate(upgradeButtonPrefab, upgradesVerticalLayoutGroup.transform);
            upgrade.Initialize(info, type);
        }

        public void CheckProgression(string upgradeNecessaryToUnlock)
        {
            var unlock = progressionUpgrades.Find(list =>
                list.UpgradeNecessaryToUnlock.Equals(upgradeNecessaryToUnlock,
                    StringComparison.InvariantCultureIgnoreCase));
            if (unlock != null)
            {
                AddUpgrades(unlock);
            }
        }

        private void AddUpgrades(UpgradeList list)
        {
            list.Upgrades.ForEach(AddUpgradeToList);
        }
    }
