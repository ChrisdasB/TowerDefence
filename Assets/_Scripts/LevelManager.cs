using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static LevelManager Instance;
    public int currentLvl;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        

        DontDestroyOnLoad(this);
    }

    public void LoadLevel(int level)
    {
        Instance.currentLvl = level;
        Time.timeScale = 1;        
        SceneManager.LoadScene(level);
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;        
        SceneManager.LoadScene(0);        
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
