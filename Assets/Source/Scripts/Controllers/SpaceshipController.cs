using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    public SpaceCraft meta;
    public float tilt = 3;

    public MovableArea boundary;

    public GameObject bullet;
    public Transform gun;

    private new Rigidbody rigidbody;
    private MeshFilter shape;
    private MeshRenderer color;
    private new MeshCollider collider;

    [Header("Oberserved Fields")]

    [SerializeField]
    private float _shootCooldown;
    //Health
    [SerializeField]
    private int _maxHealth;
    private int _currentHealth;
    [SerializeField]
    private Vector2 _movement;

    [SerializeField]
    private bool _shooting = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        shape = GetComponent<MeshFilter>();
        color = GetComponent<MeshRenderer>();
        collider = GetComponent<MeshCollider>();
        _maxHealth = meta.health;
        _currentHealth = _maxHealth;
        Debug.Log(_currentHealth + "/" + _maxHealth);
    }

    private void Update()
    {
        Shoot();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        _shooting = Mathf.Approximately(1, context.ReadValue<float>());
    }
    private void Shoot()
    {
        // decrease shooting cooldown time
        if (_shootCooldown > 0)
            _shootCooldown -= Time.deltaTime;

        // shoot!
        if (_shooting && _shootCooldown <= 0)
        {
            Instantiate(bullet, gun.position, gun.rotation);
            _shootCooldown = meta.reload;
        }
    }


    public void MoveHorizontal(InputAction.CallbackContext context)
    {
        _movement = new Vector3(context.ReadValue<float>(), _movement.y);
    }
    public void MoveVertical(InputAction.CallbackContext context)
    {
        _movement = new Vector3(_movement.x, context.ReadValue<float>());
    }
    void FixedUpdate()
    {
        rigidbody.velocity = _movement * meta.speed;

        //Rotation when flight
        rigidbody.rotation = Quaternion.Euler(-90.0f, rigidbody.velocity.x * -tilt, 0.0f);

        // Clapm object in playground
        if (rigidbody.velocity.x < 0 && rigidbody.position.x < boundary.xMin || rigidbody.velocity.x > 0 && rigidbody.position.x > boundary.xMax)
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y);
        if (rigidbody.velocity.y < 0 && rigidbody.position.y < boundary.yMin || rigidbody.velocity.y > 0 && rigidbody.position.y > boundary.yMax)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_currentHealth > 0) //
            {
                ReSpawn();
            }
            else
            {
                //gameover
                Debug.Log("Game Over");
            }
        }
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        Debug.Log(_currentHealth + "/" + _maxHealth);
    }

    public void ReSpawn()
    {
        transform.position = new Vector3(0, -6);
    }
}