using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSliding : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    private Rigidbody rigidbody;
    private PlayerMovement playerMovement;
    
    [Header("Sliding")]
    [SerializeField] private float maxSlideSpeed;
    [SerializeField] private float slideForce;
    private float slideTimer;

    [SerializeField] private float slideYScale;
    private float startYScale;
    
    [Header("Input")]
    [SerializeField] private KeyCode slideKey;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        
        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0f || verticalInput != 0f))
            StartSlide();
        
        if (Input.GetKeyUp(slideKey) && sliding)
            StopSlide();
    }

    private void StartSlide()
    {
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
    }

    private void SlidingMovement()
    {
        
    }

    private void StopSlide()
    {
        
    }
}
