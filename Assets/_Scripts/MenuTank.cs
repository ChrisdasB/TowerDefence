using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTank : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject projectilePrefab;
    float rotationSpeed = 10f;
    public bool levelChosen = false;    

    private void Awake()
    {
        if (Time.timeScale != 0)
            Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!levelChosen)
        {
            transform.rotation = ToolBox.LookAt(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition), rotationSpeed);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale == 1)
            {
                Shoot();
            }
        }
        
    }



    void Shoot()
    {
        GetComponent<AudioSource>().Play();
        Projectile freshProjectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation).GetComponent<Projectile>();

        if (freshProjectile != null)
            freshProjectile.GimmeAVector(freshProjectile.gameObject.transform.position - transform.position);
    }

    

}
