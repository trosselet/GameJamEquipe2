using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propulsion : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Transform cam;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Propulse()
    {
        if (playerMovement.isOnGround)
        {
            Vector3 forceToAdd = (-cam.transform.forward) * force;

            rb.AddForce(forceToAdd, ForceMode.Impulse);
        }    
    }
}
