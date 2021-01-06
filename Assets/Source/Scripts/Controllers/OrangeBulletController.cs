using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBulletController : MonoBehaviour
{
    public float speed = 15;
    new Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}
