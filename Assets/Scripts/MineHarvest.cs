using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHarvest : MonoBehaviour
{
    [SerializeField] private int mineHarvestPerTick = 1;
    [SerializeField] private float mineHarvestTick = 2.0f;

    private IEnumerator harvestCorountine;

    private void Awake()
    {
        harvestCorountine = HarvestCorountine();
        StartCoroutine(harvestCorountine);
    }

    private IEnumerator HarvestCorountine()
    {
        while (true)
        {
            yield return new WaitForSeconds(mineHarvestTick);
            GameLogic.Instance.IncrementMinerals(mineHarvestPerTick);
        }
    }
}
