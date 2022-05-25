using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] ParticleSystem menuExplosion;
    [SerializeField] MenuTank menuTank;
    [SerializeField] GameObject exitMenu;    
    [SerializeField] GameObject optionsMenu;    
    [SerializeField] int myLevel;
    
    [SerializeField] bool destroyAndLoad = true;
    [SerializeField] bool loadOptionsMenu = false;
    [SerializeField] bool loadExitMenu = false;

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(destroyAndLoad)
            StartCoroutine(StartLevel());

        else if(loadOptionsMenu)
        {
            optionsMenu.SetActive(true);
        }

        else if (loadExitMenu)
        {
            // activate exite menu and take away control for tank
            exitMenu.SetActive(true);            
        }
    }

    IEnumerator StartLevel()
    {
        GetComponent<AudioSource>().Play();
        menuTank.levelChosen = true;
        GetComponent<Image>().enabled = false;
        transform.GetComponentInChildren<TMP_Text>().enabled = false;
        Instantiate(menuExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        levelManager.LoadLevel(myLevel);
    }
        
        
}
