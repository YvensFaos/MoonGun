using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance;
    public static GameLogic Instance => _instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        _instance = this;
    }

    private void Start()
    {
        IncrementAsteroids(0);
        IncrementMinerals(0);
    }
    
    [Header("Game Properties")]
    [SerializeField] private int asteroidsCollected = 0;
    [SerializeField] private int mineralsCollected = 0;
    
    [Space(5)]
    
    [Header("Game Behaviours")]
    [SerializeField] private ResourcePanel resourcePanel;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    public void ToggleAsteroidSpawner(bool toggle)
    {
        asteroidSpawner.ToggleSpawner(toggle);
    }
    
    public void AsteroidDestroyed()
    {
        IncrementAsteroids(1);
    }
    
    public void IncrementAsteroids(int value)
    {
        asteroidsCollected += value;
        resourcePanel.UpdateAsteroidText(asteroidsCollected);
    }
    
    public void IncrementMinerals(int value)
    {
        mineralsCollected += value;
        resourcePanel.UpdateMineralText(mineralsCollected);
    }
}
