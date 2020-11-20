using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeList", menuName = "List of Upgrades", order = 0)]
public class UpgradeList : ScriptableObject
{
    public string UpgradeNecessaryToUnlock;
    public List<UpgradeInfo> Upgrades;
}
