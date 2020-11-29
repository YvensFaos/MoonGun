using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    [CreateAssetMenu(fileName = "AsteroidSpawnerChance", menuName = "Asteroid Spawner Chance", order = 5)]
    public class AsteroidSpawnerChance : ScriptableObject
    {
        public List<AsteroidToSpawn> asteroids;
        public float timer = 0.5f;
        public float totalChance = 0.0f;
        
        public void OnValidate() {
            totalChance = asteroids.Sum(asteroid => asteroid.spawnChance);
        }
    }
