using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    public SpaceCraft spacecraft;
    public float tilt = 3;

    public MovableArea boundary;

    public GameObject defaultBullet;
    public Transform shotSpawn;

    private new Rigidbody rigidbody;

    [Header("Oberserved Fields")]

    [SerializeField]
    private float _shootCooldown;
    [SerializeField]
    private int _health;
    [SerializeField]
    private Vector2 _movement;

    [SerializeField]
    private bool _shooting = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = spacecraft.skin;
        GetComponent<MeshFilter>().mesh = spacecraft.shape;
        GetComponent<MeshCollider>().sharedMesh = spacecraft.shape;
        _health = spacecraft.health;
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
            Instantiate(defaultBullet, shotSpawn.position, shotSpawn.rotation);
            _shootCooldown = spacecraft.reload;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Game Paused");
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
        rigidbody.velocity = _movement * spacecraft.speed;

        //Rotation when flight
        rigidbody.rotation = Quaternion.Euler(-90.0f, rigidbody.velocity.x * -tilt, 0.0f);

        // Clapm object in playground
        if (rigidbody.velocity.x < 0 && rigidbody.position.x < boundary.xMin || rigidbody.velocity.x > 0 && rigidbody.position.x > boundary.xMax)
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y);
        if (rigidbody.velocity.y < 0 && rigidbody.position.y < boundary.yMin || rigidbody.velocity.y > 0 && rigidbody.position.y > boundary.yMax)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f);

    }
}
