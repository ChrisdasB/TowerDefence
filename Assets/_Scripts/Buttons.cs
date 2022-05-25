using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioClip onMouseEnterSound;
    [SerializeField] AudioClip onMouseClickSound;

    AudioSource audioSource;

    [SerializeField] tankClass tankClass = tankClass.none;
    UIHandler uIHandler;

    

    // Grab the UIHandler
    private void Start()
    {   // Grab UIHandler
        uIHandler = FindObjectOfType<UIHandler>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // OnClick report the set class to the UIHandler
    public void ReportNewTankToUIHandler()
    {
        uIHandler.SetNewPlaceholder(tankClass);        
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(onMouseEnterSound);
    }
    
    public void PlayMouseDownSound()
    {
        audioSource.PlayOneShot(onMouseClickSound);
    }
}
