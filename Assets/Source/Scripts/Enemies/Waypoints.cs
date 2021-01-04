using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform waypoints;
    public float speed = 5f;
    public bool stop = false;

    public int current = 0;
    public Transform currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = waypoints.GetChild(current);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (transform.position != currentPoint.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
            }
            else if (current == waypoints.childCount - 1)
            {
                stop = true;
            }
            else
            {
                ++current;
                currentPoint = waypoints.GetChild(current);
            }
        }
    }
}
