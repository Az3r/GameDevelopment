using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        DestroyObject(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
