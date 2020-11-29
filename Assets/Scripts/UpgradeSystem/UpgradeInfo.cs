using System;
using UnityEngine;

[Serializable]
public struct UpgradeInfo
{
    public string name;
    [TextArea]
    public string description;
    public int mineralCost;
    public int asteroidCost;

    public UpgradeInfo(string name, string description, int mineralCost, int asteroidCost)
    {
        this.name = name;
        this.description = description;
        this.mineralCost = mineralCost;
        this.asteroidCost = asteroidCost;
    }
}
