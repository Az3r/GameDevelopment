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
    private int _shootCooldown;
    [SerializeField]
    private int _health;
    [SerializeField]
    private Vector2 _movement;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = spacecraft.skin;
        GetComponent<MeshCollider>().sharedMesh = spacecraft.shape;
    }

    private void Update()
    {
        // decrease shooting cooldown time
        if (_shootCooldown > 0)
            _shootCooldown--;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (_shootCooldown <= 0)
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

        rigidbody.MovePosition(new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
            0.0f
        ));

        //Rotation when flight
        rigidbody.rotation = Quaternion.Euler(-90.0f, rigidbody.velocity.x * -tilt, 0.0f);
    }

}
