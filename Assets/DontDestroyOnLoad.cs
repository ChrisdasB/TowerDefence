using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    DontDestroyOnLoad Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
