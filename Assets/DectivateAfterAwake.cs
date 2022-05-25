using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectivateAfterAwake : MonoBehaviour
{
    bool firstStart = true;

    [SerializeField] AudioControl musicControl;
    [SerializeField] AudioControl effectsControl;

    private void Start()
    {
        if (firstStart)
        {
            firstStart = false;
            musicControl.LoadFloat();
            musicControl.LoadBool();
            effectsControl.LoadFloat();
            effectsControl.LoadBool();
            gameObject.SetActive(false);
        }
            
    }
}
