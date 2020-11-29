﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] private Collider spawnerCollider;
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;
    [SerializeField] private bool startSpawning;
    private int _tier; //0 by default

    [Header("Asteroids to Spawn")] 
    [SerializeField] private List<AsteroidSpawnerChance> asteroidsToSpawn;
    
    [SerializeField] private  List<AsteroidToSpawn> asteroids;
    [SerializeField] private  float timer = 0.5f;
    
    [SerializeField] private List<AsteroidToSpawn> asteroidTier2;
    
    private IEnumerator _spawner;

    [Header("Debug")]
    [SerializeField] private bool debugUpdatePosition = false;
    
    void Awake()
    {
        var bounds = spawnerCollider.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }

    private void Start()
    {
        _spawner = SpawnAsteroids();
        if (startSpawning)
        {
            StartCoroutine(_spawner);    
        }

        asteroidsToSpawn.ForEach(asteroidList => asteroidList.asteroids.Sort());
    }

    public void ToggleSpawner(bool toggle)
    {
        StopCoroutine(_spawner);
        if (toggle)
        {
            StartCoroutine(_spawner);
        }
    }

    private void Update()
    {
        if (debugUpdatePosition)
        {
            var bounds = spawnerCollider.bounds;
            minBounds = bounds.min;
            maxBounds = bounds.max;
        }
    }

    private void ChanceSpawnAsteroids(AsteroidSpawnerChance asteroidList)
    {
        float chance = Random.Range(0.0f, asteroidList.totalChance);
        float chanceAcc = 0.0f;
        
        var spawnList = asteroidList.asteroids;
        // switch (_tier)
        // {
        //     case 1: spawnList = asteroidTier2;
        //         break;
        // }
            
        foreach (var asteroid in spawnList)
        {
            if (chance < asteroid.spawnChance + chanceAcc)
            {
                var asteroidObject = LeanPool.Spawn(asteroid.asteroidPrefab, 
                    new Vector3(Random.Range(minBounds.x, maxBounds.x),Random.Range(minBounds.y, maxBounds.y), Random.Range(minBounds.z, maxBounds.z)),
                    Quaternion.identity);
                asteroidObject.AddForce(Vector3.down * asteroid.asteroidFallVelocity, ForceMode.Impulse);
                // LeanPool.Despawn(asteroidObject.gameObject, asteroid.asteroidLifeDuration);
                return;
            }

            chanceAcc += asteroid.spawnChance;
        }
    }

    public void IncrementTier()
    {
        _tier++;
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            var asteroidList = asteroidsToSpawn[_tier];
            yield return new WaitForSeconds(asteroidList.timer);
            ChanceSpawnAsteroids(asteroidList);
        }
    }
}
