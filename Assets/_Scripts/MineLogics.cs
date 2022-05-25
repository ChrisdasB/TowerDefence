using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLogics : MonoBehaviour
{
    int damage = 50;    
        
    // if we hit an enemy, do damage, trigger spawning of explosion effect, destroy this GO
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemyScript = collision.gameObject.GetComponent<EnemyMovement>();
        if(enemyScript != null)
        {
            enemyScript.GetDamage(damage);
            GameManager.Instance.MineExplode(transform);
            Destroy(gameObject);
        }
    } 
}
