using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineHarvest : Upgradable
{
    [Header("Mining Properties")]
    [SerializeField] private int mineHarvestPerTick = 1;
    [SerializeField] private float mineHarvestTick = 5.0f;
    [SerializeField] private float nuggetsChance; //0 by default

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

    public void StopHarvesting()
    {
        StopCoroutine(_harvestCorountine);
    }

    public void RestoreHarvesting()
    {
        StopCoroutine(_harvestCorountine);
        StartCoroutine(_harvestCorountine);
    }

    public void UnlockUpgrade(UpgradeInfo info)
    {
        //TODO verify values before upgrade, to avoid downgrading when using outdated upgrades
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
                nuggetsChance += 5.0f;
                break;
            
            //Tier 3
            case "Mine Even and Even Faster":
                mineHarvestTick = 1.5f;
                break;
            case "Mine Even and Even Harder":
                mineHarvestPerTick = 4;
                break;
            case "Atmospheric Protective Fog":
                GameLogic.Instance.Shield.IncrementShieldStrength(1.0f);
                break;
        }
    }

    public void UpgradeNuggets(int incrementBy)
    {
        nuggetsChance += incrementBy;
    }
}
