﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    new Rigidbody rigidbody;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = -transform.up * speed;//move down
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            SpaceshipController spaceship = collider.GetComponent<SpaceshipController>();
            if (spaceship != null)
            {
                spaceship.ChangeHealth(1);
                Destroy(gameObject);
            }

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
