using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] int damage;    

    Vector3 directionVector;

    void Update()
    {
        if(directionVector != Vector3.zero)        
            transform.position += directionVector * speed * Time.deltaTime;                
    }

    // Used to receive a direction to fly after instantiation
    public void GimmeAVector(Vector3 direction)
    {
        directionVector = direction;
    }

    // Trigger the hit animation
    public void YouHit()
    {
        Destroy(gameObject);
    }

    // Return the set damage value of thi projectile
    public int GetDamageValue()
    {
        return damage;
    }
}
