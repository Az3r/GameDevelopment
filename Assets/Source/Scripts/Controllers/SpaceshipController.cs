﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public class Boundary
{
    public float xMin = -8, xMax = 8, yMin = -5, yMax = 5;
}

public class SpaceshipController : MonoBehaviour
{
    public float speed = 10;
    //private Vector2 mousePosition;
    float horizontal;
    float vertical;
    new Rigidbody rigidbody;
    public float tilt = 3;

    public Boundary boundary;

    public GameObject defaultBullet;
    public Transform shotSpawn;

    public float fireRate = 0.2f;

    private float nextFire;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(defaultBullet, shotSpawn.position, shotSpawn.rotation);
        }
    }

    public void MoveHorizontal(InputAction.CallbackContext context)
    {
        var hor = context.ReadValue<float>();
        movement = new Vector3(hor, movement.y);
    }
    public void MoveVertical(InputAction.CallbackContext context)
    {
        var ver = context.ReadValue<float>();
        movement = new Vector3(movement.x, ver);
    }
    void FixedUpdate()
    {
        //Input.GetAxis("Vertical");
        rigidbody.velocity = movement * speed;

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
