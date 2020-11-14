using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineHarvest : MonoBehaviour
{
    [Header("Upgrade Properties")]
    [SerializeField] private List<UpgradeInfo> hangarUpgrades;
    [SerializeField] private VerticalLayoutGroup upgradesList;
    [SerializeField] private UpgradeButton upgradeButton;
    
    [Header("Mining Properties")]
    [SerializeField] private int mineHarvestPerTick = 1;
    [SerializeField] private float mineHarvestTick = 5.0f;

    private IEnumerator harvestCorountine;

    private void Awake()
    {
        harvestCorountine = HarvestCorountine();
        StartCoroutine(harvestCorountine);
    }
    
    public void Start()
    {
        hangarUpgrades.ForEach(info =>
        {
            var upgrade = Instantiate(upgradeButton, upgradesList.transform);
            upgrade.Initialize(info, FacilityType.MINES);
        });
    }

    private IEnumerator HarvestCorountine()
    {
        while (true)
        {
            yield return new WaitForSeconds(mineHarvestTick);
            GameLogic.Instance.IncrementMinerals(mineHarvestPerTick);
        }
    }

    public void UnlockUpgrade(UpgradeInfo info)
    {
        if (info.name.Equals("Mine Faster"))
        {
            mineHarvestTick = 3.0f;
        } else if (info.name.Equals("Mine Harder"))
        {
            mineHarvestPerTick = 2;
        } 
    }
}
