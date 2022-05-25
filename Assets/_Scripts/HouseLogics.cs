using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLogics : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyScript))
        {
            GameManager.Instance.PlayerChangeHealth(enemyScript.enemyDamage, false);
            enemyScript.KillEnemy(false);
        }
    }



}
