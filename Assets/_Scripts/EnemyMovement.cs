using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject[] checkpoint;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] int health;       
    [SerializeField] GameObject healthBar;
        

    public int enemyDamage = 50;
    int checkpointNumber = 0;
    int MAX_HEALTH;

    lookDirection myLookDirection = lookDirection.Right;

    private void Start()
    {   // Set the health and update the healthbar after
        MAX_HEALTH = health;
        print("Max Health is: " + MAX_HEALTH);
        UpdateHealthBar();        
    }

    void Update()
    {   // if we reach the final checkpoint, destroy this GamObject
        if(checkpointNumber == checkpoint.Length)
        {
            GameManager.Instance.EnemyDied(transform, false);
            Destroy(transform.parent.gameObject);
        }

        // if we have checkpoints, look at the current standardLookDirection and move towards current checkpoint
        if (checkpoint != null)
        {
            transform.rotation = ToolBox.LookAt(transform, ToolBox.StandardLookDirection(myLookDirection, transform), rotationSpeed);
            transform.position = ToolBox.MoveTo(transform, checkpoint[checkpointNumber].transform.position, speed);            
        }

        // If no health left, destroy the parent element
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    // If we got hit by a Projectile, trigger YouHit funtion on Projectile, receive damage and update the healthbar
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Projectile projectileScript = collision.GetComponent<Projectile>();
            GetDamage(projectileScript.GetDamageValue());            
            projectileScript.YouHit();
        }
    }

    public void GetDamage(int damageValue)
    {
        health -= damageValue;
        UpdateHealthBar();        
    }

    // Increment the index of the checkpoint array, set next direction to look at
    public void TriggerNextCheckpoint(lookDirection nextDirection)
    {
        checkpointNumber++;
        myLookDirection = nextDirection;
    }

    // Receive an array of checkpoints from the gameManager after be spawned
    public void SetCheckpoints(GameObject[] checkpoints)
    {       
        checkpoint = checkpoints;        
    }

    // Update health bar to the current health
    void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = (float) health / MAX_HEALTH;
        print("Slider Value is " + (float)health / MAX_HEALTH);
    }

    public void KillEnemy(bool dropCoin = true)
    {
        GameManager.Instance.EnemyDied(transform, dropCoin);
        Destroy(transform.parent.gameObject);
    }
}
