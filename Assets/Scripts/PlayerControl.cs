using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [Header("Movement")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;

    [Header("Bullet")]
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletLocation;


    [Header("Particle")]
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private int particleCount;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        float rotationAngle = rotationInput * turnSpeed * Time.deltaTime;
        _rigidbody.transform.Rotate(Vector3.back,rotationAngle);

        float movementInput = Input.GetAxis("Vertical");
        float movementDirection = movementInput * moveSpeed * Time.deltaTime;
        _rigidbody.transform.Translate(0, movementDirection, 0);

        if (Input.GetAxis("Vertical") != 0)
        {
            _particle.Emit(particleCount);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Bullet _newBullet = Instantiate(_bullet,_bulletLocation.position,Quaternion.identity);
            _newBullet.FireBullet(_bulletLocation.up, bulletSpeed);
        }

    }

    public void OnDeath()
    {
        //Destroy player
        //Prompt retry screen or to go back to the main menu
    }
}
