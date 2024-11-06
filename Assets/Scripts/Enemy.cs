using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private EnemySpawnScript _spawnScript;

    [SerializeField] private float speed;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spawnScript = FindAnyObjectByType<EnemySpawnScript>();
        _rigidbody.AddForce(gameObject.transform.up * getSpeed(),ForceMode2D.Impulse);
        Debug.Log("Enemy started moving");
        Destroy(gameObject,30f);
    }

    public virtual void TakeDamage()
    {
        TriggerSpawnScript();
        Destroy(gameObject);
    }

    public void TriggerSpawnScript()
    {
        if (_spawnScript != null)
        {
            _spawnScript.enemyDied();
        }
    }


    public float getSpeed()
    { return speed; }

}
