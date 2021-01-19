using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifeTime;
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < 0) Destroy(this.gameObject);
        else
        {
            transform.position += direction * speed * Time.deltaTime;
            lifeTime -= Time.deltaTime;
        }
    }
}
