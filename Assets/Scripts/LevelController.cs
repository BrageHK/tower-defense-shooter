using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class LevelController : MonoBehaviour
{
    public GameObject slime;


    private int wave = 0, spawnedEnemies = 0;
    private float enemyTimer = 0, waveTimer = 0, betweenWaveTimer = 20, waveMinLength = 9999f;
    private const float endOfWaveMaxTime = 20; //After a wave is done, this is the time to the next wave
    private bool isWaveInProgress = true, moneyWasAddedThisWave = false;
    public Button button;
    public TextMeshProUGUI waveText, waveTimerText, moneyText;
    public int money = 0;
    private int[] moneyPerWave = {100, 0, 0, 0, 0, 500, 600, 700, 800, 900, 1000};
    public GameObject tower1;

 


    private void Start()
    {
        CloseBetweenWavesUI();   
    }

    private void Update() {
        //Timers
        enemyTimer += Time.deltaTime; //Time since last enemy was spawned
        

        //This is run when the wave is in progress
        if (isWaveInProgress)
        {
            EnemySpawner();
            waveTimer += Time.deltaTime; //This is the timer for the current wave
        }

        //This is run between waves
        if (!isWaveInProgress)
        {
            OpenBetweenWavesUI();
            betweenWaveTimer -= Time.deltaTime;
            if (betweenWaveTimer <= 0)
            {
                wave++;
                betweenWaveTimer = endOfWaveMaxTime;
                CloseBetweenWavesUI();
                isWaveInProgress = true;
                moneyWasAddedThisWave = false;
                spawnedEnemies = 0;
                waveTimer = 0;
            }
        }

        UpdateText();
        UpdateTowerUI();



        //When there are no enemies on screen and the timer is longer than the minimum length, set the wave status to false.
        if (GameObject.FindWithTag("Enemy") == null && waveMinLength <= waveTimer)
        {
            if(!moneyWasAddedThisWave)
            {
                money += moneyPerWave[wave];
                moneyWasAddedThisWave = true;   
            }
            isWaveInProgress = false;
        }
    }

    private void UpdateText()
    {
        waveText.text = "Wave: " + wave;
        waveTimerText.text = "Next wave in: " + Mathf.Round(betweenWaveTimer);
        moneyText.text = "Money: " + money;
    }

    public void SetBetweenWaveTimerToZero()
    {
        betweenWaveTimer = 0;
    }
    private void OpenBetweenWavesUI()
    {
        button.enabled = true;
        button.gameObject.SetActive(true);
        waveTimerText.enabled = true;
        //enable and disable more UI elements
    }

    private void CloseBetweenWavesUI()
    {
        button.enabled = false;
        button.gameObject.SetActive(false);
        waveTimerText.enabled = false;
        //enable and disable more UI elements
    }

    private void UpdateTowerUI()
    {
        if (money >= 100)
        {
            tower1.SetActive(true);
        }
        else
        {
            tower1.SetActive(false);
        }
    }

    public void EnemySpawner() {
        switch (wave) {
            case 0:
                SpawnSlimes(1, 0.4f);
                break;
            case 1:
                SpawnSlimes(8, 0.4f);
                break;
            case 2:
                SpawnSlimes(10, 0.4f);
                break;
            default: 
                SpawnSlimes(70, 0.05f);
                break;
        }
    }

    private void SpawnSlimes(int slimes, float spawnDelay)
    {
        waveMinLength = slimes * spawnDelay;
        if (spawnedEnemies >= slimes)
        {
            return;
        }

        if (enemyTimer >= spawnDelay)
        {
            GameObject slimeEnemy = Instantiate(slime, new Vector3(0, 0, 0), Quaternion.identity, transform);
            enemyTimer = 0;
            spawnedEnemies++;
        }
            
    }
    private void SpawnBiggerSlimes(int slimes) {}
    private void SpawnZombies(int zombies) {}
}
