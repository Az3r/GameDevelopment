using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon2 : MonoBehaviour
{
    private AudioSource audioSource;
    public List<Transform> gun1;
    public GameObject shot;
    public float fireRate;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire1", delay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fire1()
    {
        for (int i = 0; i < gun1.Count; i++)
        {
            Instantiate(shot, gun1[i].position, gun1[i].rotation);
        }
        audioSource.Play();

    }
}
