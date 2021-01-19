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

    //Explosion
    public GameObject explosion;
    public GameObject playerExplosion;
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
        body.velocity = new Vector3(newManeuver, currentSpeed, 0.0f);

        body.position = new Vector3(
            Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );

        //body.rotation = Quaternion.Euler(0.0f, body.velocity.x * -tilt, 0.0f);
    }
    private void OnDestroy()
    {
        //StopCoroutine(coroutine);
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            SpaceshipController spaceship = other.GetComponent<SpaceshipController>();
            if (spaceship != null)
            {
                spaceship.ChangeHealth(-1);
            }
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        else
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
