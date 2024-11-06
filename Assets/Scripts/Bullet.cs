using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    public void FireBullet(Vector3 fireLocation, float speed)
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(fireLocation * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 4f);
    }

    public Rigidbody2D getRigidbody()
        { return _rigidbody; }

}
