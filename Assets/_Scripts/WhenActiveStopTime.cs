using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenActiveStopTime : MonoBehaviour
{
    
    private void OnEnable()
    {
        Time.timeScale = 0;
    }   

    private void OnDisable()
    {
        if (Time.timeScale < 1)
            Time.timeScale = 1;
    }
}
