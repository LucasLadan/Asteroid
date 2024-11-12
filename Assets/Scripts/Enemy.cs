using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private EnemySpawnScript _spawnScript;

    private bool isActive = false;
    private bool isPaused = false;

    [SerializeField] private float speed;
    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gameObject.transform.Rotate(0, 0, Random.Range(-20f, 20f));
        _spawnScript = FindAnyObjectByType<EnemySpawnScript>();
        FindObjectOfType<EventHolder>().pause.AddListener(Paused);
        FindObjectOfType<EventHolder>().resume.AddListener(Resumed);
        _rigidbody.AddForce(gameObject.transform.up * getSpeed(),ForceMode2D.Impulse);
        Debug.Log("Enemy started moving");
    }

    public void Paused()
    {
        if (_rigidbody != null)
        {
            isPaused = true;
            _rigidbody.AddForce(gameObject.transform.up * getSpeed() * -1, ForceMode2D.Impulse);
        }
    }

    public void Resumed()
    {
        if (_rigidbody != null)
        {
            isPaused = false;
            _rigidbody.AddForce(gameObject.transform.up * getSpeed(), ForceMode2D.Impulse);
        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "background")
        {
            Destroy(gameObject);
        }
    }

    public void TriggerSpawnScript()
    {
        if (_spawnScript != null)
        {
            _spawnScript.EnemyDied();
        }
    }


    public float getSpeed()
    { return speed; }

    public bool getIsActive()
        { return isActive; }

    public void SetIsActive(bool newBool)
    { isActive = newBool; }

    public Rigidbody2D GetRigidbody() 
    { return _rigidbody; }

    public bool getPaused()
    {
        return isPaused;
    }

}
