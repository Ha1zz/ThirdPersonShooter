using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject shortRangeEnemy;
    [SerializeField] GameObject longRangeEnemy;

    [SerializeField] int numbersOfPickups;

    public int total;
    public static EnemySpawner Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        total = numbersOfPickups;
    }


    void Start()
    {
        for (int i = 0; i < numbersOfPickups; i++)
        {
            SpawnPickup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (total < numbersOfPickups)
        {
            SpawnPickup();
            total++;
        }
    }

    private void SpawnPickup()
    {
        int seed = Random.Range(0, 100);
        if (seed <= 30)
        {
            float seedA = RunNumber();
            seedA += transform.position.x;
            float seedB = RunNumber();
            seedB += transform.position.z;
            Instantiate(shortRangeEnemy, new Vector3(seedA, transform.position.y, seedB), Quaternion.identity);
        }
        else
        {
            float seedA = RunNumber();
            seedA += transform.position.x;
            float seedB = RunNumber();
            seedB += transform.position.z;
            Instantiate(longRangeEnemy, new Vector3(seedA, transform.position.y, seedB), Quaternion.identity);
        }

    }

    private float RunNumber()
    {
        return Random.Range(-50, 50);
    }
}
