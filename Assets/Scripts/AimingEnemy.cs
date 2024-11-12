using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingEnemy : ShootingEnemy
{
    [SerializeField] GameObject rotating;
    void Update()
    {
        if (!getPaused() && getIsActive())
        {
            rotating.transform.up = FindObjectOfType<PlayerControl>().transform.position - rotating.transform.position;
        }
        
    }
}
