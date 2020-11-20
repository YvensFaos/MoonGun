using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineHarvest : Upgradable
{
    [Header("Mining Properties")]
    [SerializeField] private int mineHarvestPerTick = 1;
    [SerializeField] private float mineHarvestTick = 5.0f;

    private IEnumerator _harvestCorountine;

    private void Awake()
    {
        _harvestCorountine = HarvestCorountine();
        StartCoroutine(_harvestCorountine);
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
        //Tier 1
        if (info.name.Equals("Mine Faster"))
        {
            mineHarvestTick = 3.0f;
        } else if (info.name.Equals("Mine Harder"))
        {
            mineHarvestPerTick = 2;
        }
        
        //Tier 2
        if (info.name.Equals("Mine Even Faster"))
        {
            mineHarvestTick = 2.0f;
        } else if (info.name.Equals("Mine Even Harder"))
        {
            mineHarvestPerTick = 3;
        }
    }
}
