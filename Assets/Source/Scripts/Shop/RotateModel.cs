using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    private Transform model;
    public Vector3 angle;
    private void Start()
    {
        model = GetComponent<Transform>();
    }
    void Update()
    {
        model.Rotate(angle);
    }
}
