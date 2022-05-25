using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    UIHandler uiHandler;
    SpawnManager spawnManager;
    AudioManager audioManager;

    GameObject[] checkpoint;
    List<List<int>> wavesCount = new List<List<int>>();

    List<int> level1WavesCount = new List<int>();
    List<int> level2WavesCount = new List<int>();
    List<int> level3WavesCount = new List<int>();


    public int currentWave { get; private set; } = 0;
    public int enemysLeftInWave { get; private set; }
    public float minSpawnTime { get; private set; } = 1;
    public float maxSpawnTime { get; private set; } = 5;

    static int startMoney = 50;
    public int currentScore;       

    public int tankOneCost { get; private set; } = 20;
    public int tankTwoCost { get; private set; } = 50;
    public int tankThreeCost { get; private set; } = 100;
    public int mineCost { get; private set; } = 15;

    public static int PLAYER_MAX_HEALTH = 250;
    public static int PLAYER_MIN_HEALTH = 0;
    public int playerCurrentHealth;
    int currentLevel = 1;

    private void Awake()
    {        
        Instance = this;
        audioManager = GetComponent<AudioManager>();
        if (audioManager != null)
            print("AudioManager found");
        GetWaveValues();

        
        uiHandler = GetComponent<UIHandler>();
        spawnManager = GetComponent<SpawnManager>();
        setNewScore(startMoney, true);
        GetCheckpoints();
    }
        
    void Start()
    {        
        StartCoroutine("TriggerNextWave");        
        playerCurrentHealth = PLAYER_MAX_HEALTH;
        uiHandler.UpdateHouseHealthbar();
    }        
        
    void GetWaveValues()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        DataManager dm = FindObjectOfType<DataManager>();
        if(dm != null)
        {
            wavesCount.Add(dm.level1WavesCount);
            wavesCount.Add(dm.level2WavesCount);
            wavesCount.Add(dm.level3WavesCount);
        }
        else
        {
            print("NO DATA MANAGER FOUND!");
        }

        if(lm != null)
        {
            currentLevel = lm.currentLvl;
            print("current level is: " + currentLevel);
        }
        else
        {
            print("No Level Manger Found!");
        }

    }

    // Find all available checkpoints, sort them by the value "checkPointNr", add checkpoints into checkpoint[] as gameobjects
    void GetCheckpoints()
    {
        Checkpoint[] foundCP = FindObjectsOfType<Checkpoint>();
        
        Checkpoint[] sortedFoundCP = new Checkpoint[foundCP.Length];
        
        checkpoint = new GameObject[foundCP.Length];

        for (int i = 0; i < foundCP.Length; i++)
        {
            for (int ii = 0; i < foundCP.Length; ii++)
            {
                if (foundCP[ii].checkpointNr == i + 1)
                {
                    sortedFoundCP[i] = foundCP[ii];
                    print("Checkpoint Nr. " + (i+1) + " added!");
                    break;
                }                    
            }
        }

        for (int i = 0; i < checkpoint.Length; i++)
        {
            checkpoint[i] = sortedFoundCP[i].gameObject;
        }
    }

    // Calculate the new score in dependency to a bool, trigger UIHandler to update Money-Text
    public void setNewScore(int value, bool addScore)
    {
        if (addScore)
        {
            currentScore += value;            
        }
            
        else
            currentScore -= value;
        uiHandler.UpdateScoreText(currentScore);        
    }

    public void ResetHealth()
    {
        playerCurrentHealth = PLAYER_MAX_HEALTH;
    }

    // Informs spawn mangager, that an enemy has died
    public void EnemyDied(Transform enemyTransform, bool dropCoin = true)
    {
        enemysLeftInWave--;        
        spawnManager.SpawnExplosion(enemyTransform);
        if(dropCoin)
            spawnManager.SpawnCoin(enemyTransform);
        if (enemysLeftInWave <= 0)
            StartCoroutine("TriggerNextWave");

        uiHandler.UpdateEnemyLeftText(enemysLeftInWave);
    }    

    // Trigger the spawn of the mine explostion effect from the spawn manager
    public void MineExplode(Transform mineTransform)
    {
        spawnManager.SpawnMineExplosion(mineTransform);
    }

    // Check if we have enough money for this spawn
    public bool ValidateCostSpawn(int cost)
    {
        if (cost <= Instance.currentScore)
        {
            return true;            
        }
        else
        {
            StartCoroutine(uiHandler.DisplayWarningText("Not enough money!"));
            StartCoroutine(uiHandler.placeholderWarning());
            return false;            
        }
            
    }

    // Spawns an Enemy and reports the list of checkpoints
    public void SpawnEnemy(Transform positionToSpawn)
    {
        spawnManager.SpawnEnemy(positionToSpawn, checkpoint);
    }

    // If 2 validations succeed, Spawn a mine, destroy the currentPlaceholder and update the Money
    public void TrySpawningMine(Transform positionToSpawn)
    {
        if (uiHandler.ValidateMineSpawn() && ValidateCostSpawn(mineCost))
        {
            spawnManager.SpawnMine(positionToSpawn);
            uiHandler.DestroyPlaceholder();
            setNewScore(mineCost, false);
        }
            
        else
            print("No Mine spawned!");
    }

    // Show nextWave text, increase the current wave by one, set enemyLeft to new value, update wave text in canvas;
    IEnumerator TriggerNextWave()
    {
        if (currentWave >= wavesCount[currentLevel-1].Count)
        {
            uiHandler.ShowEndScreen("You Win!");
            StopAllCoroutines();
        }
            
        spawnManager.SpawnNextWaveSign();
        yield return new WaitForSeconds(3);               
        currentWave += 1;               
        enemysLeftInWave = wavesCount[currentLevel -1][currentWave -1];
        spawnManager.enemyCountSpawn = wavesCount[currentLevel -1][currentWave -1];
        uiHandler.UpdateWaveText(currentWave);
        uiHandler.UpdateEnemyLeftText(enemysLeftInWave);
        maxSpawnTime -= 0.5f;
        minSpawnTime -= 0.1f;
    }

    public void PlayerChangeHealth(int value, bool addValue = false)
    {
        if (addValue)
            playerCurrentHealth += value;
        else
            playerCurrentHealth -= value;

        uiHandler.UpdateHouseHealthbar();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

}


