using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform carTransformCurrent;
    private GameObject car;
    private Rigidbody carRbCurrent;

    private Rigidbody cameraRB;
    
    public Vector3 Offset;
    private Vector3 playerForward;
    private Vector3 carLocalVelocity;
    private float carLocalVelocityZ;

    public float sensitivity = 150f;
    private float xRotation;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        carTransformCurrent = car.transform;
        carRbCurrent = car.GetComponent<Rigidbody>();
        cameraRB = carRbCurrent;
    }

    void Update()
    {
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        carLocalVelocity = transform.InverseTransformDirection(carRbCurrent.velocity);
        carLocalVelocityZ = carLocalVelocity.z;
        
        playerForward = (cameraRB.velocity + carTransformCurrent.transform.forward).normalized;
        
        transform.position = Vector3.Lerp(transform.position,
            carTransformCurrent.position + carTransformCurrent.transform.TransformVector(Offset)
            + playerForward * (-5f),
            carLocalVelocityZ * Time.deltaTime);
        transform.LookAt(carTransformCurrent);
    }
}
