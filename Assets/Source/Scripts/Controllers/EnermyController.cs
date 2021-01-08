using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public float speed;

    private Rigidbody rigidbody;

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    private float currentSpeed;
    private float targetManeuver;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = - transform.up * speed;
        currentSpeed = rigidbody.velocity.y;

        StartCoroutine(Evade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rigidbody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);

        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.yMin, boundary.yMax)
        );

        rigidbody.rotation = Quaternion.Euler(
            0.0f,
            
            rigidbody.velocity.x * -tilt,
            0.0f
        );
    }
}
