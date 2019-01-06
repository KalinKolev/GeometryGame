using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{    
    public GameObject[] enemyTypes;       

    float timeToSpawn = 0f;

    float spawnRate = 1.7f;

    private void Update()
    {
        if (Time.time > timeToSpawn)
        {
            Vector3 positionToSpawn = new Vector3(15, 0, 0);

            int enemyIndex = Random.Range(0, enemyTypes.Length);

            GameObject enemy = Instantiate(enemyTypes[enemyIndex], positionToSpawn, Quaternion.identity);

            enemy.GetComponent<MoveEnemy>().enemyType = enemy.tag;

            Destroy(enemy, 20f);

            timeToSpawn = Time.time + 1 / spawnRate;
        }
    }
}
