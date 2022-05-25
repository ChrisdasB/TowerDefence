using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemySpawner;

    [Header("PlayerSpawnables")]
    [SerializeField] GameObject tankPrefabLevel1;
    [SerializeField] GameObject tankPrefabLevel2;
    [SerializeField] GameObject tankPrefabLevel3;
    [SerializeField] GameObject minePrefab;

    [Header("Enemys")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyFastPrefab;
    [SerializeField] GameObject enemyStrongPrefab;
    [SerializeField] GameObject coinPrefab;
    
    
    [Header("Effects")]
    [SerializeField] ParticleSystem enemyExplosion;
    [SerializeField] ParticleSystem mineExplosion;

    [SerializeField] Image nextWaveText;

    
       

    bool isSpawning = false;
    public int enemyCountSpawn { get; set; }

    private void Update()
    {
        if (!isSpawning && enemyCountSpawn > 0)
            StartCoroutine("RandomSpawn");
    }

    // Receives the tankSpot, spawns a tank in relation to the currently spawned tank, puts it to the position of the tankSpot and sends the currently spawned object to it
    public void SpawnTank(GameObject tankSpot, tankClass currentTankClass)
    {
        GameObject prefabToSpawn;
        TankSpot currentTankScript = tankSpot.GetComponent<TankSpot>();

        if (currentTankClass == tankClass.none)        
            prefabToSpawn = tankPrefabLevel1; 
                     
        else if (currentTankClass == tankClass.Tank1)        
            prefabToSpawn = tankPrefabLevel2;
                     
        else if (currentTankClass == tankClass.Tank2) 
            prefabToSpawn = tankPrefabLevel3;
               
        else        
            prefabToSpawn = tankPrefabLevel1;      

        GameObject freshSpawn = Instantiate(prefabToSpawn, tankSpot.transform.position, Quaternion.identity);
        freshSpawn.transform.SetParent(tankSpot.transform);
        tankSpot.GetComponent<TankSpot>().SetCurrentlySpawnedObject(freshSpawn);
    }

    // Spawns a coin on the given postition
    public void SpawnCoin(Transform positionToSpawn)
    {
        Vector3 modifyZVector = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, -0.2f);
        Instantiate(coinPrefab, modifyZVector, Quaternion.identity);
    }

    // Spawn a ParticelSystem explosion on the given position
    public void SpawnExplosion(Transform positionToSpawn)
    {
        Instantiate(enemyExplosion, positionToSpawn.position, Quaternion.identity);
    }

    // Spawn mine explosion Prefab on given position
    public void SpawnMineExplosion(Transform positionToSpawn)
    {
        Instantiate(mineExplosion, positionToSpawn.position, Quaternion.identity);
    }

    // Spawns an enemy on the given position and sets its checkpoints
    public void SpawnEnemy(Transform postitionToSpawn, GameObject[] checkpoints)
    {
        GameObject enemyToSpawn;

        int randomNumber = Random.Range(0, 10);

        if (randomNumber <= 6)
        {
            enemyToSpawn = enemyPrefab;
            print(randomNumber + " normal Enemy!");
        }            
        else if (randomNumber <= 8)
        {
            print(randomNumber + " fast Enemy!");
            enemyToSpawn = enemyFastPrefab;
        }
        else
        {
            print(randomNumber + " strong Enemy!");
            enemyToSpawn = enemyStrongPrefab;
        }
            

        GameObject freshSpawn = Instantiate(enemyToSpawn, postitionToSpawn.position, Quaternion.identity);
        freshSpawn.GetComponentInChildren<EnemyMovement>().SetCheckpoints(checkpoints);        
    }

    // Spawn next wave sign
    public void SpawnNextWaveSign()
    {
        nextWaveText.GetComponent<Animator>().SetTrigger("NextWave");
    }

    // Spawn with a random waittime from the gamemanger new enemys, decrease count for enemy left to spawn
    IEnumerator RandomSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(Random.Range(GameManager.Instance.minSpawnTime, GameManager.Instance.maxSpawnTime));
        GameManager.Instance.SpawnEnemy(enemySpawner.transform);
        enemyCountSpawn--;
        isSpawning = false;
    }

    // Spawn the MinePrefab on the given position
    public void SpawnMine(Transform positionToSpawn)
    {
        Instantiate(minePrefab, positionToSpawn.position, Quaternion.identity);
    }

}
