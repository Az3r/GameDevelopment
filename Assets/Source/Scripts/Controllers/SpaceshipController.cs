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
    public float keyspeed = 10;
    public float mousespeed = 30;
    //move by key
    float keyHorizontal;
    float keyVertical;
    //move by mouse
    float mouseHorizontal;
    float mouseVertical;
    bool mouseMove;


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
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            mouseMove = true;
            //Move by mouse
            mouseHorizontal = Input.GetAxis("Horizontal");
            mouseVertical = Input.GetAxis("Vertical");

            Vector3 movementbykey = new Vector3(mouseHorizontal, mouseVertical, 0.0f);
            rigidbody.velocity = movementbykey * keyspeed;

            rigidbody.MovePosition(new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
                0.0f
            ));

            //Rotation when flight
            rigidbody.rotation = Quaternion.Euler(-90.0f, rigidbody.velocity.x * -tilt, 0.0f);
        } else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !mouseMove)
        {
            //Move by key
            keyHorizontal = Input.GetAxis("Horizontal");
            keyVertical = Input.GetAxis("Vertical");

            Vector3 movementbykey = new Vector3(keyHorizontal, keyVertical, 0.0f);
            rigidbody.velocity = movementbykey * keyspeed;

            rigidbody.MovePosition(new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
                0.0f
            ));

            //Rotation when flight
            rigidbody.rotation = Quaternion.Euler(-90.0f, rigidbody.velocity.x * -tilt, 0.0f);
        }
        else
        {
            mouseMove = false;
        }
        
    }
       
         
}
