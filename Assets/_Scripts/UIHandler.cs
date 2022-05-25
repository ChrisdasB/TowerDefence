using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    [Header("Text")]
    
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text enemysLeftText;    
    [SerializeField] TMP_Text warningText;

    [Header("Images")]
    [SerializeField] Image Buttonlevel1;
    [SerializeField] Image Buttonlevel2;
    [SerializeField] Image Buttonlevel3;
    [SerializeField] Image ButtonMine;

    [Header("PlaceholderPrefabs")]
    [SerializeField] GameObject placeholderPrefab;    
    [SerializeField] GameObject warningBox;

    [Header("Menus")]
    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject gameEndingMenu;
    [SerializeField] TMP_Text gameEndingText;

    
    List<SpriteRenderer> mineSpawnerList = new List<SpriteRenderer>();

    [SerializeField] GameObject houseHealthBar;

    [Header("Audio")]
    AudioClip coinPickUp;

    Color buttonAvailableColor = Color.green;
    Color buttonUnavailableColor = Color.red;

    GameObject currentPlaceholder;

    SpawnManager spawnManager;

    bool placeholderLoggedIn = false;
    bool backgroundIsActive = false;

    private void Start()
    {        
        menuBackground.SetActive(false);
        gameEndingMenu.SetActive(false);
        if (spawnManager == null)
            spawnManager = GetComponent<SpawnManager>();
        warningBox.SetActive(false);

        GetMineSpawner();
    }

    private void Update()
    {// if we have a placeholder, move it to the mouse position
        if (currentPlaceholder != null && !placeholderLoggedIn)
            ToolBox.FollowMouse(currentPlaceholder.gameObject);  
        
     // If we have a right click, destroy the current placeholder
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DestroyPlaceholder();            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!backgroundIsActive)
            {
                menuBackground.SetActive(true);
                backgroundIsActive = true;
                Time.timeScale = 0;
            }

            else
            {
                menuBackground.SetActive(false);
                backgroundIsActive = false;
                Time.timeScale = 1;
            }
        }

        if (GameManager.Instance.playerCurrentHealth <= 0)
        {
            GameManager.Instance.ResetHealth();
            ShowEndScreen("You Lose!");
        }
            

    }

    private void FixedUpdate()
    {
        CheckButtonColor();
    }

    void GetMineSpawner()
    {
        MineSpawner[] foundMineSpawner = FindObjectsOfType<MineSpawner>();
        foreach (var item in foundMineSpawner)
        {
            mineSpawnerList.Add(item.GetComponent<SpriteRenderer>());
        }
    }

    // Destroy the current placeholder, deactivate visibility of mine spawner
    public void DestroyPlaceholder()
    {
        Destroy(currentPlaceholder);
        ResetMineSpots();
    }

    // Update the color of the button in dependecy of the cost
    private void CheckButtonColor()
    {
        if (GameManager.Instance.currentScore >= GameManager.Instance.tankOneCost)
            Buttonlevel1.color = buttonAvailableColor;
        else        
            Buttonlevel1.color = buttonUnavailableColor;            
        

        if (GameManager.Instance.currentScore >= GameManager.Instance.tankTwoCost)
            Buttonlevel2.color = buttonAvailableColor;
        else        
            Buttonlevel2.color = buttonUnavailableColor;            
        

        if (GameManager.Instance.currentScore >= GameManager.Instance.tankThreeCost)
            Buttonlevel3.color = buttonAvailableColor;
        else        
            Buttonlevel3.color = buttonUnavailableColor;

        if (GameManager.Instance.currentScore >= GameManager.Instance.mineCost)
            ButtonMine.color = buttonAvailableColor;
        else
            ButtonMine.color = buttonUnavailableColor;
    }

    // Destroy the current placeholder, instantiate a new one an parse through its class
    public void SetNewPlaceholder(tankClass newTankClass)
    {
        Destroy(currentPlaceholder);
        currentPlaceholder = Instantiate(placeholderPrefab, transform.position, Quaternion.identity);
        currentPlaceholder.GetComponent<Placeholder>().SetTankClass(newTankClass);
        if (newTankClass == tankClass.Mine)
            HighlightMineSpots();
        else
            ResetMineSpots();
    }     

    // Log the currentplaceholder to the tank spot. if the class is not Mine
    public void LogOnTarget(GameObject logOnSpot)
    {        
        if(currentPlaceholder != null && currentPlaceholder.TryGetComponent<Placeholder>(out Placeholder objectScript))
            {
            if (currentPlaceholder != null && objectScript.tankClass != tankClass.Mine && !logOnSpot.GetComponent<TankSpot>().forMines)
            {
                currentPlaceholder.transform.position = logOnSpot.transform.position;
                placeholderLoggedIn = true;
            }            
        }        
    }

    // Unlock the currentplaceholder from the tank spot
    public void LofOffTarget()
    {
        placeholderLoggedIn = false;
    }

    // If the current Placeholder is a Mine, log it onto the given position
    public void LogInMine(Transform positionToLogOn)
    {
        if (currentPlaceholder != null && currentPlaceholder.TryGetComponent<Placeholder>(out Placeholder objectScript))
        {
            if (currentPlaceholder != null && objectScript.tankClass == tankClass.Mine)
            {
                currentPlaceholder.transform.position = positionToLogOn.position;
                placeholderLoggedIn = true;
            }
        }
    }

    // Unlock the Placeholder from any fix position
    public void LogOffMine()
    {
        placeholderLoggedIn = false;        
    }

    // Validate, if we have a placeholder atm, if yes, validate the correct spawn, if yes, report to spawnManager and destroy the placeholder
    public void ValidationSpawnTank(GameObject tankSpot, tankClass currentTankClass, int costs)
    {        
        if(placeholderLoggedIn && currentPlaceholder != null)
        {
            if (ValidateTankClass(currentTankClass, currentPlaceholder.GetComponent<Placeholder>().tankClass))
            {
                if (GameManager.Instance.ValidateCostSpawn(costs))
                {
                    tankSpot.GetComponent<TankSpot>().SoundAllowedDenied(true);
                    GameManager.Instance.setNewScore(costs, false);
                    spawnManager.SpawnTank(tankSpot, currentTankClass);
                    Destroy(currentPlaceholder);
                    placeholderLoggedIn = false;
                }                
            }
            else
            {
                tankSpot.GetComponent<TankSpot>().SoundAllowedDenied(false);
            }           
        }
    }

    // Update the score Text to the send value
    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    // Update the wave Text to the send value
    public void UpdateWaveText(int value)
    {
        waveText.text = value.ToString();
    }

    // Update the enemyLeft Text to the send value
    public void UpdateEnemyLeftText(int value)
    {
        enemysLeftText.text = value.ToString(); 
    }

    // Check the current and the attempt tankclass for a valid spawn
    bool ValidateTankClass(tankClass currentTankClass, tankClass tankClassToCheck)
    {
        if (currentTankClass == tankClass.none && tankClassToCheck == tankClass.Tank1)
            return true;
        else if (currentTankClass == tankClass.Tank1 && tankClassToCheck == tankClass.Tank2)
            return true;
        else if (currentTankClass == tankClass.Tank2 && tankClassToCheck == tankClass.Tank3)
            return true;        
        else
        {
            StartCoroutine(placeholderWarning());
            StartCoroutine(DisplayWarningText("Wrong level for this Spawn!"));
            return false;
        }
    }

    // Return tru, if the current placeholder is a mine
    public bool ValidateMineSpawn()
    {
        if(currentPlaceholder.TryGetComponent<Placeholder>(out Placeholder placeholderScript))
        {
            if (currentPlaceholder != null && placeholderScript.tankClass == tankClass.Mine)            
                return true;
            
            else
                return false;
        }
        else
            return false;
    }

    // Mark the placeholder red, if spawn attempt was not valid
    public IEnumerator placeholderWarning()
    {
        print("no valid Spawn attempt");
        Color placeholderColor = currentPlaceholder.GetComponent<SpriteRenderer>().color;

        currentPlaceholder.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1);

        currentPlaceholder.GetComponent<SpriteRenderer>().color = placeholderColor;
    }

    // Display custom warning text at the top of the display
    public IEnumerator DisplayWarningText(string textToDisplay)
    {
        warningBox.SetActive(true);
        warningText.text = textToDisplay;
        yield return new WaitForSeconds(2);
        warningText.text = "";
        warningBox.SetActive(false);
    }

    // Set the SpriteRenderer of all mine spots in the list acitve
    void HighlightMineSpots()
    {
        for (int i = 0; i < mineSpawnerList.Count; i++)
        {
            if (mineSpawnerList[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer objectRenderer))
                objectRenderer.enabled = true;
        }
    }

    // Set the SpriteRenderer of all mine spots in the list inacitve
    void ResetMineSpots()
    {
        for (int i = 0; i < mineSpawnerList.Count; i++)
        {
            if (mineSpawnerList[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer objectRenderer))
                objectRenderer.enabled = false;
        }
    }

    public void UpdateHouseHealthbar()
    {               
            houseHealthBar.GetComponent<Slider>().value = (float)GameManager.Instance.playerCurrentHealth / GameManager.PLAYER_MAX_HEALTH;        
    }

    public void ShowEndScreen(string myText)
    {        
        gameEndingMenu.SetActive(true);
        Time.timeScale = 0;
        gameEndingText.text = myText;
    }

    
}
