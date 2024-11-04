using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private string target;
    private Rigidbody2D _rigidbody;

    public void FireBullet(Vector3 fireLocation, float speed)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(fireLocation * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == target)
        {
            //Destroy target
            Destroy(gameObject);
        }
    }

}
