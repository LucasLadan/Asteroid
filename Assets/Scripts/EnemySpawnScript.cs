using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [SerializeField] private int chanceToSpawn;

    private int difficulty = 2;//This will change

    private float totalWeight = 200f;
    private float currentWeight = 0f;

    private float tier1Meteor = 200f;
    private float tier2Meteor = 0f;
    private float tier3Meteor = 0f;
    private float tier1Shooter = 0f;
    private float tier2Shooter = 0f;
    private float tier1Quad = 0f;
    private float tier2Quad = 0f;

    [SerializeField] private Transform spawnLocation;

    [Header("Enemies")]
    [SerializeField] private Meteor _meteorT1;
    [SerializeField] private Meteor _meteorT2;
    [SerializeField] private Meteor _meteorT3;

    private void Start()
    {
        
    }

    private void Update()
    {
        float rand = 0f;

        gameObject.transform.Rotate(0, 0, Random.Range(0, 360));

        if (Random.Range(0,chanceToSpawn) == 1)
        {

            Debug.Log("Enemy tried");
            rand = Random.Range(0f,totalWeight);
            Debug.Log("Enemy tried " + rand);
            if (rand < tier1Meteor)
            {
                Meteor meteor = Instantiate(_meteorT1,spawnLocation.position,spawnLocation.rotation);
            }
            else if (rand < tier1Meteor - tier2Meteor )
            {
                Meteor meteor = Instantiate(_meteorT2, spawnLocation.position, spawnLocation.rotation);
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor)
            {
                Meteor meteor = Instantiate(_meteorT3, spawnLocation.position, spawnLocation.rotation);
            }
        }
    }

    public void enemyDied()
    {
        tier1Meteor -= 2.2f;
        tier2Meteor += 0.6f;
        tier3Meteor += 0.4f;

        switch(difficulty)
        {
            case 1:
                tier1Shooter += 0.6f;
                tier1Quad += 0.6f;
                break;
            case 2:
                tier1Shooter += 0.4f;
                tier2Shooter += 0.2f;
                tier1Quad += 0.4f;
                tier2Quad += 0.2f;
                break;
            case 3:
                tier2Shooter += 0.6f;
                tier2Quad += 0.6f;
                break;
        }

    }
}
