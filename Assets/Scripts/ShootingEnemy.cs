using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy
{

    [SerializeField] private List<Transform> shootingLocations;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireSpeed;

   

    new void Start()
    {
        StartCoroutine(Shoot());
        base.Start();
        
    }

    IEnumerator Shoot()
    {
        if (!getPaused() && getIsActive())
        {
            for (int i = 0; i < shootingLocations.Count; i++)
            {
                Bullet newBullet = Instantiate(_bullet, shootingLocations[i].position, shootingLocations[i].rotation);
                newBullet.FireBullet(shootingLocations[i].transform.up, fireSpeed);
            }
        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Shoot());
    }
}
