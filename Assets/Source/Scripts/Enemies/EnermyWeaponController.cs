﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyWeaponController : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
            
    }
}
