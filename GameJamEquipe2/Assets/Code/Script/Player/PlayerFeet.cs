using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CinemachineImpulseSource impulseSource;
    
    private void Start()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
        impulseSource = transform.parent.GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        impulseSource.GenerateImpulse(0.4f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") == false) return;
        
        playerMovement.onGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") == false) return;
        
        playerMovement.onGround = false;
    }
}
