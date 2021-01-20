using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public int value;
    public float lifeTime;
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    private void Start()
    {
        GetComponent<TMPro.TextMeshPro>().text = $"+ {value.ToString()}";
    }
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
