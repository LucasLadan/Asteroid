using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class NonAimingEnemies : ShootingEnemy
{
    [SerializeField] GameObject rotating;
    void Update()
    {
        if (!getPaused() && getIsActive())
        {
            rotating.transform.Rotate(0,0,5,Space.Self);
        }
    }
}
