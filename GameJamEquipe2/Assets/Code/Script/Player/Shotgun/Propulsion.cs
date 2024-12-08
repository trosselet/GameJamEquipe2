using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propulsion : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Transform cam;
    [SerializeField] private Animation recoil;
    [SerializeField] private ParticleSystem bulletSystem;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    private bool hasJump = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.readyToJump && playerMovement.isOnGround) hasJump = true;
    }

    public void Propulse()
    {
        if (playerMovement.readyToJump && hasJump)
        {
            Vector3 forceToAdd = (-cam.transform.forward) * force;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
            hasJump = false;
            recoil.Play();
            bulletSystem.Play();
            if (playerMovement.isOnGround) playerMovement.readyToJump = false;
        }
    }
}
