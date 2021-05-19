using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] GameObject[] EnemiesTypes;
    public static int EnemiesSpawned=0;
    float SpawnRate = 8;
    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void Update()
    {
        SpawnRate -= Time.deltaTime / 50;
    }

    IEnumerator Spawning()
    {
        yield return null;
        while (true)
        {
            if(!Player.single || Player.single.died)
            {
                EnemiesSpawned = 0;
                break;
            }
            if (EnemiesSpawned >= 12)
            {
                yield return null;
                continue;
            }
            Instantiate(EnemiesTypes[Random.Range(0, EnemiesTypes.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(SpawnRate);
        }
    }

}
