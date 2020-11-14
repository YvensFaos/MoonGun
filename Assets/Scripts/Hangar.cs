using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hangar : MonoBehaviour
{
    [SerializeField] private List<UpgradeInfo> hangarUpgrades;

    [SerializeField] private VerticalLayoutGroup upgradesList;

    [SerializeField] private UpgradeButton upgradeButton;

    public void Start()
    {
        hangarUpgrades.ForEach(info =>
        {
            var upgrade = Instantiate(upgradeButton, upgradesList.transform);
            upgrade.Initialize(info, FacilityType.HANGAR);
        });
    }

    public void UnlockUpgrade(UpgradeInfo info)
    {
        if (info.name.Equals("Faster Shoot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeProjectileForce(8.0f);
        } else if (info.name.Equals("Bigger Projectile"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeProjectileScale(new Vector3(0.095f, 0.095f, 0.095f));
        } else if (info.name.Equals("Faster Shoot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeCooldown(0.75f);
        }
    }
}
