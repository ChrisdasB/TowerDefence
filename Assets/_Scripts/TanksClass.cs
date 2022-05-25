using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TanksClass : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject projectilePrefab;
    
    [SerializeField] AudioClip shootingSound;
    [SerializeField] public tankClass myTankClass;
    [SerializeField] lookDirection lookDirection = lookDirection.Left;

    GameObject currentTarget;
    AudioSource audioSource;

    float rotationSpeed = 10f;
    float shootingDelay = 1.5f;
    float currentShootingDelay = 1f;
    float initalShootingDelay = 0.5f;
    float aggroRadius = 4f;

    bool isShooting = false;
        
    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }
    
    void Update()
    {        
        // If we can shoot, shot if timer is done, or reset timer
        if(isShooting)
        {
            if (currentShootingDelay <= 0)
            {
                Shoot();
                currentShootingDelay = shootingDelay;
            }
            else
                currentShootingDelay -= Time.deltaTime;
        }

        // Look for an enemy
        SearchForEnemy();
    }

    // Draw Gizmo of the aggroRadius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
    
    // Draw an OverlapCircle and look for an enemy
    void SearchForEnemy()
    {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));        
        
        // If there is an enemy, so the array is not empty, check if we have a current target, else set the first object in the array to current target
        if (hitCollider.Length >= 1)
        {
            if (currentTarget == null)            
                currentTarget = hitCollider[0].gameObject;            
            // Loop through the collider array and look if our current target is in it
            int countdown = 0;
            for (int i = 0; i < hitCollider.Length; i++)
            {// If it is in the array, start attacking and rotate towards it
                if (currentTarget == hitCollider[i].gameObject)
                {                    
                    transform.rotation = ToolBox.LookAt(transform, hitCollider[i].transform.position, rotationSpeed);
                    isShooting = true;                    
                }// if no in array, count the failures
                else
                {
                    countdown++;
                }
            }
            //if the failure are equaliy long as the array, we set a new target
            if (countdown == hitCollider.Length)            
                currentTarget = hitCollider[0].gameObject;
        }
        // if we dont have found enemy, reset current target, stop shooting, reset shooting timer and rotate towards the standard postition
        else if (hitCollider.Length == 0)
        {
            currentTarget = null;            
            isShooting = false;
            currentShootingDelay = initalShootingDelay;
            transform.rotation = ToolBox.LookAt(transform, ToolBox.StandardLookDirection(lookDirection, transform));            
        }            
    }

    //Instantiate a projectile, grab the Projectile-Script, and if we got it, give the Projectile its direction to fly
    void Shoot()
    {
        Projectile freshProjectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation).GetComponent<Projectile>();
        audioSource.PlayOneShot(shootingSound);
        if(freshProjectile != null)
            freshProjectile.GimmeAVector(freshProjectile.gameObject.transform.position - transform.position);            
    }
}
