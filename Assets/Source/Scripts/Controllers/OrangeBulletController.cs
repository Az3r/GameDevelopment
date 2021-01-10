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

    private void OnBecameInvisible()
    {
        Debug.Log("Destroy Bullets");
        Destroy(gameObject);

    }
}
