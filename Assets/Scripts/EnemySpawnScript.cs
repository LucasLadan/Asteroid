using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [SerializeField] EventHolder _eventHolder;

    [SerializeField] private int chanceToSpawn;
    [SerializeField] private int waitBetweenWaves;

    [SerializeField] private int minimumToSpawn;
    [SerializeField] private int chanceToSpawnBounce;

    private int difficulty = 2;//This will change
    [SerializeField] private bool notPaused = true;


    private float tier1Meteor = 400f;
    private float tier2Meteor = 0f;
    private float tier3Meteor = 0f;
    private float tier4Meteor = 0f;
    private float tier1Shooter = 0f;
    private float tier2Shooter = 0f;
    private float tier1Burst = 0f;
    private float tier2Burst = 0f;

    [SerializeField] private GameObject spawnLocation;

    [Header("enemies")]
    [SerializeField] private Meteor _meteorT1;
    [SerializeField] private Meteor _meteorT2;
    [SerializeField] private Meteor _meteorT3;
    [SerializeField] private Meteor _meteorT4;

    [SerializeField] private Bouncy _bounceT1;
    [SerializeField] private Bouncy _bounceT2;
    [SerializeField] private Bouncy _bounceT3;
    [SerializeField] private Bouncy _bounceT4;

    [SerializeField] private ShootingEnemy _aimT1;
    [SerializeField] private ShootingEnemy _aimT2;
    [SerializeField] private ShootingEnemy _burstT1;
    [SerializeField] private ShootingEnemy _burstT2;


    [Header("Score")]
    [SerializeField] private int score = 0;
    [SerializeField] private int maxScore;

    private void Start()
    {

        if (FindObjectOfType<DifficultyHolder>())
        {
            difficulty = FindObjectOfType<DifficultyHolder>().getDifficulty();
        }

        _eventHolder.UpdateScore(score,maxScore);
        _eventHolder.pause.AddListener(Paused);
        _eventHolder.resume.AddListener(Resumed);
        StartCoroutine(waveSpawner());
    }

    private void Update()
    {
        float rand = 0f;

        gameObject.transform.Rotate(0, 0, Random.Range(0f, 360f));



        if (notPaused && Random.Range(0,chanceToSpawn) <= minimumToSpawn)
        {
            rand = Random.Range(0f,tier1Meteor);
            Debug.Log("Enemy tried " + rand);
            if (rand > tier1Meteor - tier2Meteor)//2nd tier meteor
            {
                if (Random.Range(1,chanceToSpawn) != 2)
                {
                    Meteor meteor = Instantiate(_meteorT2, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
                else
                {
                    Bouncy bounce = Instantiate(_bounceT2, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }    
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor)//3rd tier meteor
            {
                if (Random.Range(1, chanceToSpawn) != 2)
                {
                    Meteor meteor = Instantiate(_meteorT3, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
                else
                {
                    Bouncy bounce = Instantiate(_bounceT3, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor - tier1Shooter)//1st tier aim
            {
                ShootingEnemy shooter = Instantiate(_aimT1, spawnLocation.transform.position, spawnLocation.transform.rotation);
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor - tier1Shooter - tier2Shooter)//2nd tier aim
            {
                ShootingEnemy shooter = Instantiate(_aimT2, spawnLocation.transform.position, spawnLocation.transform.rotation);
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor -tier1Shooter -tier2Shooter -tier1Burst)//1st tier burst
            {
                ShootingEnemy shooter = Instantiate(_burstT1, spawnLocation.transform.position, spawnLocation.transform.rotation);
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor - tier1Shooter - tier2Shooter - tier1Burst - tier2Burst)//2nd tier burst
            {
                ShootingEnemy shooter = Instantiate(_burstT2, spawnLocation.transform.position, spawnLocation.transform.rotation);
            }
            else if (rand > tier1Meteor - tier2Meteor - tier3Meteor - tier1Shooter - tier2Shooter - 
                tier1Burst - tier2Burst - tier4Meteor)//4th tier meteor
            {
                if (Random.Range(1, chanceToSpawn) != 2)
                {
                    Meteor meteor = Instantiate(_meteorT4, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
                else
                {
                    Bouncy bounce = Instantiate(_bounceT4, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
            }
            else//1st tier meteor
            {
                if (Random.Range(1, chanceToSpawn) != 2)
                {
                    Meteor meteor = Instantiate(_meteorT1, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
                else
                {
                    Bouncy bounce = Instantiate(_bounceT1, spawnLocation.transform.position, spawnLocation.transform.rotation);
                }
            }
            
            
        }
    }

    public void Paused()
    {
        notPaused = false;
    }

    public void Resumed()
    {
        notPaused = true;
    }


    public void EnemyDied()
    {

        score += 1;
        _eventHolder.UpdateScore(score,maxScore);
        gameObject.GetComponent<AudioSource>().Play();

        if (score > maxScore)
        {
            _eventHolder.YouWin();
            return;
        }

        chanceToSpawn -= 1;

        switch(difficulty)
        {
            case 1:
                tier2Meteor += 0.6f;
                tier3Meteor += 0.4f;
                tier1Shooter += 0.6f;
                tier1Burst += 0.6f;
                break;
            case 2:
                tier2Meteor += 0.5f;
                tier3Meteor += 0.3f;
                tier4Meteor += 0.2f;
                tier1Shooter += 0.4f;
                tier2Shooter += 0.2f;
                tier1Burst += 0.4f;
                tier2Burst += 0.2f;
                break;
            case 3:
                tier3Meteor += 0.6f;
                tier4Meteor += 0.4f;
                tier2Shooter += 0.6f;
                tier2Burst += 0.6f;
                break;
        }

        if (Random.Range(0,60) == 1)
        {
            StartCoroutine(DoSurge(60,1f));
        }

    }

    public void Restart()
    {
        DifficultyHolder _difficultyHolder = FindObjectOfType<DifficultyHolder>();
        if (_difficultyHolder != null)
        {
            _difficultyHolder.startGame(difficulty);
        }
    }


    IEnumerator waveSpawner()
    {
        
        for (int tempWaitTime = waitBetweenWaves; tempWaitTime > 0;)
        {
            do
            {
                yield return new WaitForSeconds(1f);
            } while (!notPaused);

            tempWaitTime -= 1;

        }
        DoSurge(30, 3f);
        minimumToSpawn += 1;

        waveSpawner();
    }

    IEnumerator DoSurge(int spawnRate, float waitTime)
    {
        int heldMin = minimumToSpawn;
        minimumToSpawn = spawnRate;
        Debug.Log("Surge "+minimumToSpawn);
        yield return new WaitForSeconds(waitTime);
        minimumToSpawn = heldMin;
        Debug.Log("Surge ended");
    }
}
