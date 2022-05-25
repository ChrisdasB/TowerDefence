using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] lookDirection nextDirection = lookDirection.Up;
    [SerializeField] public int checkpointNr;

    // If we got hit by the trigger of the enemy, Increment checkpoint-index
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Trigger")
        {
            EnemyMovement enemy = collision.transform.parent.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemy.TriggerNextCheckpoint(nextDirection);
        }            
    }
}
