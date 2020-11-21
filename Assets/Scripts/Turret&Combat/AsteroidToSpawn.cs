using System;
using UnityEngine;

[Serializable]
public struct AsteroidToSpawn : IComparable
{
    public Rigidbody asteroidPrefab;
    public float asteroidLifeDuration;
    public float spawnChance;
    public float asteroidFallVelocity;
    
    public int CompareTo(object obj)
    {
        var asteroid = (AsteroidToSpawn) obj;
        return spawnChance.CompareTo(asteroid.spawnChance);
    }
}
