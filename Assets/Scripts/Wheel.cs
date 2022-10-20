using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private WheelCollider wheelCol;
    private Transform wheelMesh;
    public float rpm;

    private void Start()
    {
        wheelCol = GetComponentInChildren<WheelCollider>();
        wheelMesh = transform.Find("WheelMeshHolder");
        rpm = wheelCol.rpm;
    }

    public void Accelerate(float powerInput)
    {
        if (powerInput > 0)
        {
            wheelCol.motorTorque = powerInput;
        }
        else
        {
            wheelCol.brakeTorque = 0;
        }
    }

    private void SyncMesh()
    {
        Vector3 wheelPos = transform.position;
        Quaternion wheelRot = transform.rotation;

        wheelCol.GetWorldPose(out wheelPos, out wheelRot);
        wheelMesh.transform.position = wheelPos;
        wheelMesh.transform.rotation = wheelRot;
    }
}
