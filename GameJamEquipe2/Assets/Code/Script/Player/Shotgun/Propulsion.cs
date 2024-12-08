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
    private PlayerJumping playerJumping;
    private Rigidbody rb;
    private bool hasJump = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJumping = GetComponent<PlayerJumping>();
    }

    private void Update()
    {
        if (playerJumping.readyToJump && playerMovement.onGround) hasJump = true;
    }

    public void Propulse()
    {
        if (playerJumping.readyToJump && hasJump)
        {
            Vector3 forceToAdd = (-cam.transform.forward) * force;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
            hasJump = false;
            recoil.Play();
            bulletSystem.Play();
            if (playerMovement.onGround) playerJumping.readyToJump = false;
        }
    }
}
