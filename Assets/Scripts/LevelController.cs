using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LevelController : MonoBehaviour
{
    public GameObject slime;


    private int wave = 0;
    private float enemyTimer = 0;
    private float waveTimer = 0;
    private float endOfWaveMaxTime = 20; //After a wave is done, this is the time to the next wave
    private int spawnedSlimes = 0;
    private bool isWaveInProgress = false;
    private float waveMinLength = 9999f;
    public Button button;


    private void Start()
    {
        CloseBetweenWavesUI();   
        isWaveInProgress = true;
    }

    private void Update() {
        enemyTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;

        //When there are no enemies on screen and the timer is longer than the minimum lenght, set the wavestatus to false.
        if (GameObject.FindWithTag("Enemy") == null && waveMinLength <= waveTimer)
        {
            isWaveInProgress = false;
        }

        //This is run when the wave is in progress
        if (isWaveInProgress)
        {
            EnemySpawner();
        }

        //This is run between waves
        if (!isWaveInProgress)
        {
            OpenBetweenWavesUI();
            endOfWaveMaxTime -= Time.deltaTime;

            if (endOfWaveMaxTime <= 0)
            {
                
                StartNextWave();
                
            }
        }


    }

    private void OpenBetweenWavesUI()
    {
        button.interactable = true;
        //enable and disable more UI elements
    }

    private void CloseBetweenWavesUI()
    {
        button.interactable = false;
        //enable and disable more UI elements
    }

    public void EnemySpawner() {
        isWaveInProgress = true;
        switch (wave) {
            case 0:
                SpawnSlimes(5, 0.4f);
                break;
            case 1:
                SpawnSlimes(8, 0.4f);
                break;
            case 2:
                SpawnSlimes(10, 0.4f);
                break;
        }
    }

    public void StartNextWave()
    {
        endOfWaveMaxTime = 20;
        EnemySpawner();
        spawnedSlimes = 0;
        wave++;
        CloseBetweenWavesUI();
    }



    private void SpawnSlimes(int slimes, float spawnDelay)
    {
        waveMinLength = slimes * spawnDelay;
        if (spawnedSlimes >= slimes)
        {

            return;
        }

        if (enemyTimer >= spawnDelay)
        {
            GameObject slimeEnemy = Instantiate(slime, new Vector3(0, 0, 0), Quaternion.identity, transform);
            enemyTimer = 0;
            spawnedSlimes++;
        
        }
            
    }
    private void SpawnBiggerSlimes(int slimes) {}
    private void SpawnZombies(int zombies) {}
}
