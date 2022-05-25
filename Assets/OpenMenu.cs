using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [SerializeField] GameObject menuToOpen;

    public void OpenMyMenu()
    {
        menuToOpen.SetActive(true);
    }
}
