using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(defaultBullet, shotSpawn.position, shotSpawn.rotation);
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);
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
