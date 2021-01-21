using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    private AudioSource audioSource;
    public List<Transform> gun1;
    public List<Transform> gun2;
    public List<GameObject> shot;
    public float fireRate;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire1", delay, fireRate);
        InvokeRepeating("Fire2", delay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire1()
    {
        for (int i =0; i< gun1.Count; i++)
        {
            Instantiate(shot[0], gun1[i].position, gun1[i].rotation);
        }
        audioSource.Play();

    }

    void Fire2()
    {
        for (int i = 0; i < gun2.Count; i++)
        {
            Instantiate(shot[1], gun2[i].position, gun2[i].rotation);

        }
        audioSource.Play();

    }
}
