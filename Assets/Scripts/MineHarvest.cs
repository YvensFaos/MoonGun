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

    public int MineHarvestPerTick => mineHarvestPerTick;

    public float MineHarvestTick => mineHarvestTick;

    public float NuggetsChance => nuggetsChance;

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
            yield return new WaitForSeconds(MineHarvestTick);
            int nuggetsIncrement = Random.Range(0.0f, 100.0f) < NuggetsChance ? 1 : 0;
            GameLogic.Instance.IncrementMinerals(MineHarvestPerTick + nuggetsIncrement);
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
                nuggetsChance = NuggetsChance + 5.0f;
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
            
            //Tier 4
            case "Unstoppable Miner":
                mineHarvestTick = 1.0f;
                break;
            case "Unbreakable Miner":
                mineHarvestPerTick = 5;
                break;
            case "Greater Treasure Area":
                GameLogic.Instance.TreasureControl.IncrementTreasureCapacity(3);
                break;
        }
    }

    public void UpgradeNuggets(int incrementBy)
    {
        nuggetsChance = NuggetsChance + incrementBy;
    }
}
