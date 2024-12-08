using System.Collections;
using System.Collections.Generic;
using Code.Script;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    private void Start()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
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
