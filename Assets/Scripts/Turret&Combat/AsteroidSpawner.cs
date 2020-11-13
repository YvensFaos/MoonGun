using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AsteroidSpawner : MonoBehaviour
{
    public Collider spawnerCollider;

    public Vector3 minBounds;
    public Vector3 maxBounds;

    public Rigidbody asteroidGameObject;
    public float asteroidLife = 1.0f;

    public float timer = 0.5f;

    private IEnumerator spawner;

    public bool debugUpdatePosition = false;
    
    void Awake()
    {
        var bounds = spawnerCollider.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }

    private void Start()
    {
        spawner = SpawnAsteroids();
        StartCoroutine(spawner);
    }

    public void ToggleSpawner(bool toggle)
    {
        StopCoroutine(spawner);
        if (toggle)
        {
            StartCoroutine(spawner);
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

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            var asteroid = Instantiate(asteroidGameObject, new Vector3(Random.Range(minBounds.x, maxBounds.x), 
                    Random.Range(minBounds.y, maxBounds.y), Random.Range(minBounds.z, maxBounds.z)),
                Quaternion.identity);
            asteroid.AddForce(Vector3.down * 1.0f, ForceMode.Impulse);
            Destroy(asteroid.gameObject, asteroidLife);
        }
    }
}
