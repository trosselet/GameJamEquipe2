using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateurScript : MonoBehaviour
{

    [SerializeField] private GameObject mPlayer;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == mPlayer)
        {
            return;
        }
        mPlayer.transform.position = transform.position;
        Destroy(gameObject);
    }
}