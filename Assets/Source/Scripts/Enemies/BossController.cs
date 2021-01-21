using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
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

    public bool isManeuver = true;

    //Explosion
    public GameObject explosion;
    public GameObject playerExplosion;

    //Score
    public GameObject floatingText;
    public int scoreValue;
    public Transform scorePosition;

    public int n_hit = 50;
    private int hit = 0;

    //audio
    public AudioClip audioClip;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        source = gameObject.AddComponent<AudioSource>();
        audioClip = GetComponent<AudioClip>();
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
        if (isManeuver)
        {
            float newManeuver = Mathf.MoveTowards(body.velocity.x, targetManeuver, Time.deltaTime * smoothing);
            body.velocity = new Vector3(newManeuver, currentSpeed, 0.0f);

            body.position = new Vector3(
                Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax),
                0.0f
            );
        }
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
        if (hit < n_hit)
        {
            hit++;
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
            var obj = Instantiate(floatingText, scorePosition.position, Quaternion.identity);
            GameController.Instance.AddScore(scoreValue);
            obj.GetComponent<FloatingText>().value = scoreValue;
            Destroy(gameObject);
            GameObject[] enermies = GameObject.FindGameObjectsWithTag("Enemy");
            source.PlayOneShot(audioClip);
            foreach (GameObject enermy in enermies)
            {
                Destroy(enermy);
            }
            GameController.Instance.DiplayWinningScreen();
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    
}
