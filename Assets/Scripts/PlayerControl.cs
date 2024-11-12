using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isNotShooting = true;
    private bool notPaused = true;

    [SerializeField] private float invincibilityTime;
    [SerializeField] private int health;
    [SerializeField] private EventHolder _eventHolder;
    private bool invincible = false;

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
        _eventHolder.UpdateHealth(health);
        _rigidbody = GetComponent<Rigidbody2D>();
        FindObjectOfType<EventHolder>().pause.AddListener(Paused);
        FindObjectOfType<EventHolder>().resume.AddListener(Resumed);
    }

    // Update is called once per frame
    void Update()
    {
        if (notPaused)
        {
            float rotationInput = Input.GetAxis("Horizontal");
            float rotationAngle = rotationInput * turnSpeed * Time.deltaTime;
            _rigidbody.transform.Rotate(Vector3.back, rotationAngle);

            float movementInput = Input.GetAxis("Vertical");
            float movementDirection = movementInput * moveSpeed * Time.deltaTime;
            _rigidbody.transform.Translate(0, movementDirection, 0);

            if (Input.GetAxis("Vertical") != 0)
            {
                _particle.Emit(particleCount);
            }

            if (isNotShooting && Input.GetButton("Fire1"))
            {
                StartCoroutine(FireBullet());
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
    IEnumerator FireBullet()
    {
        Bullet _newBullet = Instantiate(_bullet, _bulletLocation.position, _bulletLocation.rotation);
        _newBullet.FireBullet(_bulletLocation.up, bulletSpeed);
        isNotShooting = false;
        yield return new WaitForSeconds(fireRate);
        isNotShooting = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                health -= 1;
                _eventHolder.UpdateHealth(health);
                enemy.TakeDamage();
                StartCoroutine(Invincibility());
                if (health < 1)
                {
                    _eventHolder.YouLose();
                    GetComponent<AudioSource>().Play();
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    ParticleSystem.MainModule _mainModule = _particle.main;
                    _mainModule.startColor = Color.gray;
                    _particle.Emit(20);
                }
                return;
            }


            if (collision.gameObject.CompareTag("Enemy"))
            {
                health -= 1;
                _eventHolder.UpdateHealth(health);
                Destroy(collision);
                StartCoroutine(Invincibility());
                if (health < 1)
                {
                    _eventHolder.YouLose();
                }
                return;
            }
        }
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
