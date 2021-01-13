using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public float speed;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = -transform.up * speed;//move down
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 40.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            SpaceshipController spaceship = other.GetComponent<SpaceshipController>();
            if(spaceship != null)
            {
                spaceship.ChangeHealth(-1);
            }
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        //Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
