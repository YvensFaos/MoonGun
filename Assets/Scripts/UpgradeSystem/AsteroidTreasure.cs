using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityTemplateProjects.UpgradeSystem;

public class AsteroidTreasure : MonoBehaviour
{
    private int _uncollectedTreasures;
    
    [SerializeField] private int uncollectedTreasuresCapacity = 1;
    [SerializeField] private List<GameObject> treasuresInTheMap;
    [SerializeField] private TreasureChances treasureChances;
    [SerializeField] private ShortDialogue treasureDialogue;
    
    public void GenerateTreasureFromAsteroid()
    {
        if (_uncollectedTreasures < uncollectedTreasuresCapacity)
        {
            var unreleasedTreasure = treasuresInTheMap.Find(gameObj => !gameObj.activeSelf);
            if (unreleasedTreasure != null)
            {
                unreleasedTreasure.SetActive(true);
                _uncollectedTreasures++;    
            }
        }
    }

    //Called by the treasure game objects!
    public void CollectTreasure(int index)
    {
        if (_uncollectedTreasures > 0)
        {
            //Treasure object deactivate itself on click!
            _uncollectedTreasures--;
            treasureDialogue.gameObject.SetActive(true);
            var treasureMessage = GenerateRandomTreasure();
            treasureDialogue.AnimateText(treasureMessage);
        }
    }

    private string GenerateRandomTreasure()
    {
        var totalChances = treasureChances.NuggetChance + treasureChances.AmplifyAngleChance +
                           treasureChances.OnlyResourcesChance + treasureChances.CannonCooldownReduceChance;
        var chance = Random.Range(0.0f, totalChances);

        StringBuilder treasurePhrase = new StringBuilder();
        if (chance < treasureChances.NuggetChance)
        {
            var increase = Random.Range((int)treasureChances.NuggetValueRange.x, (int)treasureChances.NuggetValueRange.y + 1);
            treasurePhrase.AppendFormat("Nugget Chance increased by {0}%!", increase);
            GameLogic.Instance.MineControl.UpgradeNuggets(increase);
        } else if (chance < treasureChances.AmplifyAngleChance)
        {
            var increase = Random.Range(treasureChances.AmplifyAngleValueRange.x, treasureChances.AmplifyAngleValueRange.y);
            treasurePhrase.AppendFormat("Turret Angle improved by {0.000} degrees!", increase);
            GameLogic.Instance.Turret.IncrementRotationRange(increase);
        } else if (chance < treasureChances.CannonCooldownReduceChance)
        {
            var decrease = Random.Range(treasureChances.CannonCooldownReduceValueRange.x, treasureChances.CannonCooldownReduceValueRange.y);
            treasurePhrase.AppendFormat("Cannon cooldown reduced by {0.000} seconds!", decrease);
            GameLogic.Instance.Turret.DecrementCannonCooldown(decrease);
        }
        else
        {
            var asteroids = Random.Range((int)treasureChances.ResourcesValueRange.x, (int)treasureChances.ResourcesValueRange.y + 1);
            var minerals = Random.Range((int)treasureChances.ResourcesValueRange.x, (int)treasureChances.ResourcesValueRange.y + 1);
            treasurePhrase.AppendFormat("Collected {0} asteroid fragments and {1} minerals!", asteroids, minerals);
            GameLogic.Instance.IncrementAsteroids(asteroids);
            GameLogic.Instance.IncrementMinerals(minerals);
        }
        
        return treasurePhrase.ToString();
    }

    public void IncrementTreasureCapacity(int newCapacity)
    {
        uncollectedTreasuresCapacity = newCapacity;
    }
}
