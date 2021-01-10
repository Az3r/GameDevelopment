using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public float speed;

    private Rigidbody body;

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public MovableArea boundary;
    private float currentSpeed;
    private float targetManeuver;

    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.velocity = -transform.up * speed;
        currentSpeed = body.velocity.y;

        coroutine = StartCoroutine(Evade());
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
        float newManeuver = Mathf.MoveTowards(body.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        body.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);

        body.position = new Vector3(
            Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(body.position.z, boundary.yMin, boundary.yMax)
        );

        body.rotation = Quaternion.Euler(
            0.0f,

            body.velocity.x * -tilt,
            0.0f
        );
    }
    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }
}
