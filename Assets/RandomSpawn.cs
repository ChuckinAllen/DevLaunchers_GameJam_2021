using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] int watterSpawnHeight = 10;
    [SerializeField] GameObject[] spawnableObjects;
    [SerializeField] int spawnAmount = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < spawnAmount; x++)
        {
            for (int i = 0; i < spawnableObjects.Length; i++)
            {
                int randomeRange = Random.Range(0, 10);
                int RandomNumberX = Random.Range(randomeRange, randomeRange);
                int RandomNumberY = Random.Range(randomeRange, randomeRange);
                Debug.LogError(randomeRange);
                Vector3 RandomlySpawnedPos = new Vector3(RandomNumberX, watterSpawnHeight, RandomNumberY);
                Debug.Log(RandomlySpawnedPos);
                Instantiate(spawnableObjects[i], RandomlySpawnedPos, Quaternion.identity);
            }
        }
    }
}
