using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Vector3 savedFireLocaiton;
    private float savedSpeed;

    private void Start()
    {
        FindObjectOfType<EventHolder>().pause.AddListener(Paused);
        FindObjectOfType<EventHolder>().resume.AddListener(Resumed);
    }

    public void FireBullet(Vector3 fireLocation, float speed)
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        savedFireLocaiton = fireLocation;
        savedSpeed = speed;
        _rigidbody.AddForce(fireLocation * speed, ForceMode2D.Impulse);
    }

    public void Paused()
    {
        if (_rigidbody != null && savedFireLocaiton != null)
        {
            _rigidbody.AddForce(savedFireLocaiton * savedSpeed * -1, ForceMode2D.Impulse);
        }
    }

    public void Resumed()
    {
        if (_rigidbody != null && savedFireLocaiton != null)
        {
            _rigidbody.AddForce(savedFireLocaiton * savedSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "background")
        {
            Destroy(gameObject);
        }
    }

    public Rigidbody2D getRigidbody()
        { return _rigidbody; }

}
