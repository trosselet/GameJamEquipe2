using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private bool rotationX;
    [SerializeField] private bool rotationY;
    [SerializeField] private bool rotationZ;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (rotationX)
            transform.Rotate(speed, 0, 0);
        if (rotationY)
            transform.Rotate(0, speed, 0);
        if (rotationZ)
            transform.Rotate(0, 0, speed);
    }
}
