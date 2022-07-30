using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class LevelController : MonoBehaviour
{
    public GameObject slime;


    private int wave = 0;
    private float enemyTimer = 0;
    private float waveTimer = 0;
    private float betweenWaveTimer = 20;
    private const float endOfWaveMaxTime = 20; //After a wave is done, this is the time to the next wave
    private int spawnedEnemies = 0;
    private bool isWaveInProgress = true;
    private float waveMinLength = 9999f;
    public Button button;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveTimerText;

    //Cursor sprite
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;


    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
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
                spawnedEnemies = 0;
                waveTimer = 0;
            }
        }

        UpdateText();



        //When there are no enemies on screen and the timer is longer than the minimum length, set the wave status to false.
        if (GameObject.FindWithTag("Enemy") == null && waveMinLength <= waveTimer)
        {
            isWaveInProgress = false;
        }
    }

    private void UpdateText()
    {
        waveText.text = "Wave: " + wave;
        waveTimerText.text = "Next wave in: " + Mathf.Round(betweenWaveTimer);
    }

    public void SetBetweenWaveTimerToZero()
    {
        betweenWaveTimer = 0;
    }
    private void OpenBetweenWavesUI()
    {
        button.enabled = true;
        waveTimerText.enabled = true;
        //enable and disable more UI elements
    }

    private void CloseBetweenWavesUI()
    {
        button.enabled = false;
        waveTimerText.enabled = false;
        //enable and disable more UI elements
    }

    public void EnemySpawner() {
        
        Debug.Log(isWaveInProgress);
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
                SpawnSlimes(2, 0.4f);
                break;
        }
    }

    private void SpawnSlimes(int slimes, float spawnDelay)
    {
        waveMinLength = slimes * spawnDelay;
        if (spawnedEnemies >= slimes)
        {
            Debug.Log("Spawned Slimes: " + spawnedEnemies);
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
