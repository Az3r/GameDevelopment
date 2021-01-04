using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCamera : MonoBehaviour
{
  public float left;
  public float right;
  public float speed;

  [SerializeField]
  private int direction;

  private float xRotation;
  private float zRotation;

  private void Start()
  {
    // this object is also a scence controller
    Application.targetFrameRate = 60;

    // 0 <= eulerAngles.y < 360
    direction = transform.rotation.eulerAngles.y > right ? -1 : 1;
    xRotation = transform.rotation.eulerAngles.x;
    zRotation = transform.rotation.eulerAngles.z;
  }

  void Update()
  {
    // Rotate camera around the Y axis, from left to right and vice-versa
    float yRotation = transform.rotation.eulerAngles.y + speed * direction * Time.deltaTime;
    transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    if (yRotation > right && direction == 1) direction = -1;
    else if (yRotation < left && direction == -1) direction = 1;
  }
}
