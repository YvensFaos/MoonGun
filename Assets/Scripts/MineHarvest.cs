using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineHarvest : Upgradable
{
    [Header("Mining Properties")]
    [SerializeField] private int mineHarvestPerTick = 1;
    [SerializeField] private float mineHarvestTick = 5.0f;
    [SerializeField] private float nuggetsChance = 0.0f;

    private IEnumerator _harvestCorountine;

    private void Awake()
    {
        _harvestCorountine = HarvestCorountine();
    }

    public void StartMining()
    {
        StartCoroutine(_harvestCorountine);
    }
    
    private IEnumerator HarvestCorountine()
    {
        while (true)
        {
            yield return new WaitForSeconds(mineHarvestTick);
            int nuggetsIncrement = Random.Range(0.0f, 100.0f) < nuggetsChance ? 1 : 0;
            GameLogic.Instance.IncrementMinerals(mineHarvestPerTick + nuggetsIncrement);
        }
    }

    public void UnlockUpgrade(UpgradeInfo info)
    {
        switch (info.name)
        {
            //Tier 1
            case "Mine Faster": 
                mineHarvestTick = 3.0f;
                break;
            case "Mine Harder":
                mineHarvestPerTick = 2;
                break;
            //Tier 2
            case "Mine Even Faster": 
                mineHarvestTick = 2.0f;
                break;
            case "Mine Even Harder":
                mineHarvestPerTick = 3;
                break;
            case "Nuggets!":
                nuggetsChance = 5.0f;
                break;
        }
    }
}
