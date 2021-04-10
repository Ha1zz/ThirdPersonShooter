using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField] GameObject ammoPickup;
    [SerializeField] GameObject potionPickup;

    [SerializeField] int numbersOfAmmoPickup;
    [SerializeField] int numbersOfPotionPickup;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numbersOfAmmoPickup; i++)
        {
            SpawnPickup();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPickup()
    {
        int seed = Random.Range(0, 100);
        if (seed <= 50)
        {
            float seedA = RunNumber();
            seedA += transform.position.x;
            float seedB = RunNumber();
            seedB += transform.position.z;
            Instantiate(ammoPickup, new Vector3(seedA, transform.position.y, seedB),Quaternion.identity);
        }
        else
        {
            float seedA = RunNumber();
            seedA += transform.position.x;
            float seedB = RunNumber();
            seedB += transform.position.z;
            Instantiate(potionPickup, new Vector3(seedA, transform.position.y, seedB), Quaternion.identity);
        }

    }

    private float RunNumber()
    {
        return Random.Range(-50, 50);
    }
}
