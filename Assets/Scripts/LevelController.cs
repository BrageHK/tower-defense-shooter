using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject slime;


    private int wave = 0;
    private float time = 0;
    private int level = 0;

    private void Update() {
        time += Time.deltaTime;

        if (GameObject.FindWithTag("Enemy") == null) {
            wave++;
            StartNewWave();
        }


    }
   
    public void StartNewWave() {

        switch (wave) {
            case 0:
                SpawnSlimes(5, 0.3f);
                break;
            default:
                SpawnSlimes(8, 0.2f);
                break;
        }
        wave++;
    }

    private void SpawnSlimes(int slimes, float spawnDelay) {
        int spawnedSlimes = 0;
        //while (spawnedSlimes < slimes) {
        //if (time >= spawnDelay) {
            time = 0;
            GameObject enemy = Instantiate(slime, new Vector3(0, 0, 0), Quaternion.identity, transform);
               
            spawnedSlimes++;
        
    }
    private void SpawnBiggerSlimes(int slimes) {}
    private void SpawnZombies(int zombies) {}
}
