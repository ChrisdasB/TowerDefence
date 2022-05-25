using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpot : MonoBehaviour
{
    [SerializeField] AudioClip deniedAudio;
    [SerializeField] AudioClip allowedAudio;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    Color standardColor;

    int tankOneCost;
    int tankTwoCost;
    int tankThreeCost;
    int mineCost;

    public bool forMines = false;

    GameObject currentlySpawned;

    public tankClass currentTankClass { get; private set; } = tankClass.none;
    UIHandler uIHandler;

    bool mousIsOver = false;
        
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetTankCosts();
        uIHandler = FindObjectOfType<UIHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        standardColor = spriteRenderer.color;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {// if we have a click while the mouse is over, trigger validations in UIMangager
        if (Input.GetKeyDown(KeyCode.Mouse0) && mousIsOver)
            TryToSpawnTank();
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = new Color(0.6f, 0.6f, 0.6f);
        uIHandler.LogOnTarget(gameObject);
        mousIsOver = true;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = standardColor;
        uIHandler.LofOffTarget();
        mousIsOver = false;
    }    

    // Reveives the currently spawned tank from SpawnManager, and destroys the old one, if there is one
    public void SetCurrentlySpawnedObject(GameObject newSpawnedObject)
    {
        if (currentlySpawned != null)
            Destroy(currentlySpawned);

        currentlySpawned = newSpawnedObject;
        currentTankClass = newSpawnedObject.GetComponent<TanksClass>().myTankClass;
    }

    // Get the costs of spawning and report to UIHandler
    void TryToSpawnTank()
    {
        int costToSpawn;

        if (currentTankClass == tankClass.none)
            costToSpawn = tankOneCost;
        else if (currentTankClass == tankClass.Tank1)
            costToSpawn = tankTwoCost;
        else if (currentTankClass == tankClass.Tank2)
            costToSpawn = tankThreeCost;
        else if (currentTankClass == tankClass.Mine)
            costToSpawn = mineCost;
        else
        {
            costToSpawn = 100000000;
            print("No valid Tank-Class!");
        }
        print(currentTankClass);
        uIHandler.ValidationSpawnTank(gameObject, currentTankClass, costToSpawn);
    }

    // Get current tank cost from GameManger
    void GetTankCosts()
    {
        tankOneCost = GameManager.Instance.tankOneCost;
        tankTwoCost = GameManager.Instance.tankTwoCost;
        tankThreeCost = GameManager.Instance.tankThreeCost;
        mineCost = GameManager.Instance.mineCost;
    }

    public void SoundAllowedDenied(bool isAllowed)
    {
        if (isAllowed)
            audioSource.PlayOneShot(allowedAudio);
        else
            audioSource.PlayOneShot(deniedAudio);
    }
}
