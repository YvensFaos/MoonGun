using UnityEngine;

namespace UnityTemplateProjects.UpgradeSystem
{
    [CreateAssetMenu(fileName = "TreasureChances", menuName = "TreasureChances", order = 3)]
    public class TreasureChances : ScriptableObject
    {
        public float OnlyResourcesChance;
        public Vector2 ResourcesValueRange;
        
        public float CannonCooldownReduceChance;
        public Vector2 CannonCooldownReduceValueRange;
        
        public float AmplifyAngleChance;
        public Vector2 AmplifyAngleValueRange;

        public float NuggetChance;
        public Vector2 NuggetValueRange;
    }
}