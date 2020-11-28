using UnityEngine;

public class Hangar : Upgradable
{
    public void UnlockUpgrade(UpgradeInfo info)
    {
        
        var turret = GameLogic.Instance.Turret;
        switch (info.name)
        {
             //Tier 1
             case "Faster Shoot":
                 turret.UpgradeProjectileForce(8.0f);
                 break;
             case "Bigger Projectile":
                 turret.UpgradeProjectileScale(new Vector3(0.195f, 0.195f, 0.195f));
                 break;
             case "Super Reload":
                 turret.UpgradeCannonCooldown(1.00f);
                 break;
             
             case "Wide Arc":
                 turret.IncrementRotationRange(10.0f);
              break;
             case "Laser Shot":
                 turret.UnlockLaser();
                 GameLogic.Instance.DisplayWeaponPanel();
                 break;

             //Tier 2
            case "Speed Bullet":
                 turret.UpgradeProjectileForce(12.0f);
                 break;
            case "Super Charge":
                 turret.UpgradeCannonCooldown(0.75f);
                 break;
            case "Pristine Laser":
                 turret.UpgradeLaserDuration(1.5f);
                 break;
            case "Laser Recharge":
                 turret.UpgradeLaserCooldown(1.5f);
                 break;
            
             //Tier 3
             case "Celera Bullet":
                 turret.UpgradeProjectileForce(18.0f);
                 break;
             case "Hyper Charge":
                 turret.UpgradeCannonCooldown(0.5f);
                 break;
             case "Triple Cannon":
                 turret.UnlockTripleCannon();
                 break;
        }
    }
}
