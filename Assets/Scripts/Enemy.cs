using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private EnemySpawnScript _spawnScript;
    private bool isActive = false;

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
        if (getIsActive())
        {
            TriggerSpawnScript();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "background")
        {
            isActive = true;
        }
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

    public bool getIsActive()
        { return isActive; }

}
