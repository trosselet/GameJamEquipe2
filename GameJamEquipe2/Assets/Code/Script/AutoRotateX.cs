using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoRotation : MonoBehaviour
{

    [SerializeField] private float speedRotationX;
    [SerializeField] private float speedRotationY;
    [SerializeField] private float speedRotationZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        transform.Rotate(speedRotationX, speedRotationY, speedRotationZ);
    }
}