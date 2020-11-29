using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class InformationViewer : MonoBehaviour
{
    private Text _informationText;

    private void Awake()
    {
        _informationText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("General Stats\n\n");
        stringBuilder.AppendFormat("Turret\n");
        var turret = GameLogic.Instance.Turret;
        stringBuilder.AppendFormat("Rotation Range: {0}˚\n", turret.RotationRange);
        stringBuilder.AppendFormat("Turret - Cannon\n");
        stringBuilder.AppendFormat("Cannon Force: {0} P.\n", turret.ProjectileForce);
        stringBuilder.AppendFormat("Cannon Cooldown: {0} secs.\n", turret.CannonCoolDownTimer);
        stringBuilder.AppendFormat("Projectile Duration: {0} secs.\n", turret.ProjectileLife);
        stringBuilder.AppendFormat("Projectile Scale: {0:0.000} m.\n", turret.ProjectileScale);

        if (turret.UnlockedLaser)
        {
            stringBuilder.AppendFormat("Turret - Laser\n");
            stringBuilder.AppendFormat("Laser Duration: {0} secs.\n", turret.LaserConsuption);
            stringBuilder.AppendFormat("Laser Cooldown: {0} secs.\n", turret.LaserCooldown);
        }
        
        var mines = GameLogic.Instance.MineControl;
        stringBuilder.AppendFormat("\n");
        stringBuilder.AppendFormat("Mining Facility:\n");
        stringBuilder.AppendFormat("Minerals per Tick: {0} minerals.\n", mines.MineHarvestTick);
        stringBuilder.AppendFormat("Time per Tick: {0:0.00} seconds.\n", mines.MineHarvestTick);
        stringBuilder.AppendFormat("Nuggets Chance: {0:0.00}%.\n", mines.NuggetsChance);
        
        var shield = GameLogic.Instance.Shield;
        stringBuilder.AppendFormat("\n");
        stringBuilder.AppendFormat("Shield:\n");
        stringBuilder.AppendFormat("Shield Integrity: {0:0.00}%.\n", (shield.NormalizedIntegrity() * 100.0f));
        stringBuilder.AppendFormat("Shield Defense: {0} P.\n", shield.ShieldStrength);

        _informationText.text = stringBuilder.ToString();
    }
}
