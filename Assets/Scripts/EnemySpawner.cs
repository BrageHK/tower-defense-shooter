using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 0;
    int spawnedEnemies = 0;

    // Runs a timer to keep track of when to spawn enemies
    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Spawns an ememy after spawnInterval ammount of time.
    public void spawnEnemy(GameObject enemy, int numberOfEnemies, float spawnInterval, float spawnDelay)
    {
        // Returns if timer is less than initial spawn delay.
        if(timer <= spawnDelay)
        {
            return;
        }

        if(numberOfEnemies >= spawnedEnemies)
        {
            if(timer >= spawnInterval)
            {
                spawnedEnemies++;
                timer = 0;
                Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity, transform);
            }
        }
        else
        {
            // Destorys object after spawning is finished.
            Destroy(this);
        }
        
    }
}
