using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    //Explosion
    //public GameObject explosion;
    public GameObject playerExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Debug.Log("atk");
            SpaceshipController spaceship = other.GetComponent<SpaceshipController>();
            if (spaceship != null)
            {
                spaceship.ChangeHealth(-1);
            }
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        else
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
