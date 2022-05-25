using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    bool mouseIsOver = false;
    UIHandler uIHandler;

    private void Start()
    {
        uIHandler = FindObjectOfType<UIHandler>(); 
    }

    private void Update()
    {
        if(mouseIsOver && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameManager.Instance.TrySpawningMine(transform);
        }
    }

    private void OnMouseEnter()
    {
        mouseIsOver = true;
        uIHandler.LogInMine(transform);
    }

    private void OnMouseExit()
    {
        mouseIsOver = false;
        uIHandler.LogOffMine();
    }
    
}
