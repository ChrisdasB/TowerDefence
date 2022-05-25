using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] GameObject menuToClose;    

    public void CloseMyMenu()
    {
        if (menuToClose != null)
        {
            menuToClose.SetActive(false);            
        }
            
        else print("Please assign a Menu GameObject!");
    }
}
