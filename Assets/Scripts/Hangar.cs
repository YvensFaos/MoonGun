using UnityEngine;

public class Hangar : Upgradable
{
    public void UnlockUpgrade(UpgradeInfo info)
    {
        //Tier 1
        if (info.name.Equals("Faster Shoot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeProjectileForce(8.0f);
        } else if (info.name.Equals("Bigger Projectile"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeProjectileScale(new Vector3(0.195f, 0.195f, 0.195f));
        } else if (info.name.Equals("Faster Shoot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeCannonCooldown(1.00f);
        } else if (info.name.Equals("Laser Shot"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UnlockLaser();
            GameLogic.Instance.DisplayWeaponPanel();
        }
        
        //Tier 2
        if (info.name.Equals("Speed Bullet"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeProjectileForce(12.0f);
        } else if (info.name.Equals("Super Charge"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeCannonCooldown(0.75f);
        } else if (info.name.Equals("Pristine Laser"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeLaserDuration(1.0f);
        } else if (info.name.Equals("Laser Recharge"))
        {
            var turret = GameLogic.Instance.Turret;
            turret.UpgradeLaserCooldown(1.5f);
        }
    }
}
