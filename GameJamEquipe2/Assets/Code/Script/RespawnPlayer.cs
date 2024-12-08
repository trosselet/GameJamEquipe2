using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform transformRespawn;
    [SerializeField] private DimensionShifter dimensionShifter;
    // Start is called before the first frame update
    void Start()
    {
        dimensionShifter = GetComponent<DimensionShifter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OutOfMap")
        {
            transform.position = transformRespawn.transform.position;
        }
        else if (other.tag == "Respawn")
        {
            transformRespawn = other.GetComponent<Transform>();

        }
    }
}
