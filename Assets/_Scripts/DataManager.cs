using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] public List<int> level1WavesCount = new List<int>();
    [SerializeField] public List<int> level2WavesCount = new List<int>();
    [SerializeField] public List<int> level3WavesCount = new List<int>();



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    

}
