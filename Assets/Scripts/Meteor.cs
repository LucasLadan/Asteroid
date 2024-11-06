using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy
{
    [SerializeField] private Meteor nextTier;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;

    public override void TakeDamage()
    {

        if (getIsActive())
        {
            TriggerSpawnScript();
            if (nextTier != null)
            {
                Meteor meteor1 = Instantiate(nextTier, spawn1.position, spawn1.rotation);
                Meteor meteor2 = Instantiate(nextTier, spawn2.position, spawn2.rotation);
            }
            Destroy(gameObject);
        } 
    }
}
