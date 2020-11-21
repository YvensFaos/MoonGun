using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] private  Collider spawnerCollider;
    [SerializeField] private  Vector3 minBounds;
    [SerializeField] private  Vector3 maxBounds;

    [Header("Asteroids to Spawn")]
    [SerializeField] private  List<AsteroidToSpawn> asteroids;
    [SerializeField] private  float timer = 0.5f;
    private float _totalChance;
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
        StartCoroutine(_spawner);

        asteroids.Sort();
        _totalChance = asteroids.Sum(asteroid => asteroid.spawnChance);
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

    private void ChanceSpawnAsteroids()
    {
        float chance = Random.Range(0.0f, _totalChance);
        float chanceAcc = 0.0f;
        foreach (var asteroid in asteroids)
        {
            if (chance < asteroid.spawnChance + chanceAcc)
            {
                var asteroidObject = Instantiate(asteroid.asteroidPrefab, new Vector3(Random.Range(minBounds.x, maxBounds.x), 
                        Random.Range(minBounds.y, maxBounds.y), Random.Range(minBounds.z, maxBounds.z)),
                    Quaternion.identity);
                asteroidObject.AddForce(Vector3.down * asteroid.asteroidFallVelocity, ForceMode.Impulse);
                Destroy(asteroidObject.gameObject, asteroid.asteroidLifeDuration);
                return;
            }

            chanceAcc += asteroid.spawnChance;
        }
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            ChanceSpawnAsteroids();
        }
    }
}
