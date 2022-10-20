using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle2bScript : MonoBehaviour
{
    public bool escape_front;
    public bool seekLight;
    public bool cruiseStraight;
    public bool batteryPowered;
    public float baseMotorTorque;
    public float gravity;

    private Rigidbody rb;
    private BumperScript frontBumperScript;
    private Wheel wheelLeft;
    private Wheel wheelRight;
    private lightCheckScript lightScriptLeft;
    private lightCheckScript lightScriptRight;
    private float powerInputLeft = 0.0f;
    private float powerInputRight = 0.0f;

    void Start()
    {
        escape_front = true;
        seekLight = true;
        cruiseStraight = true;
        batteryPowered = true;
        baseMotorTorque = 100f;
        gravity = 9.81f;

        rb = GetComponent<Rigidbody>();
        frontBumperScript = this.transform.Find("Bumpers").transform.Find("FrontBumper").GetComponent<BumperScript>();
        wheelLeft = this.transform.Find("Wheels").transform.Find("WheelLeft").GetComponent<Wheel>();
        wheelRight = this.transform.Find("Wheels").transform.Find("WheelRight").GetComponent<Wheel>();
        lightScriptLeft = this.transform.Find("LightSensors").transform.Find("LightSensorLeft").GetComponent<lightCheckScript>();
        lightScriptRight = this.transform.Find("LightSensors").transform.Find("LightSensorRight").GetComponent<lightCheckScript>();
    }

    void FixedUpdate()
    {
        //escape_front
        if (escape_front)
        {
            if (frontBumperScript.collided)
            {
                float rpmInSec = (wheelLeft.rpm + wheelRight.rpm) / (2.0f * 60.0f);
                float backingTime = 5 * (1 / rpmInSec);
                float rotation = Random.value * 360;
                float timer = 0.0f;
                //float rotationTorqueL = (rb.mass * (2 ^ 2) * rotation) / (2 * (3 ^ 2));
                //float rotationTorqueR = (-1) * rotationTorqueL;

                while (timer <= backingTime)
                {
                    timer += Time.deltaTime;
                    powerInputLeft *= (-1);
                    powerInputRight *= (-1);
                }

                this.transform.Rotate(0.0f, rotation, 0.0f, Space.Self);
            }

            frontBumperScript.collided = false;
        }

        if (seekLight)
        {
            powerInputLeft += lightScriptRight.lightCoefficient * lightScriptRight.lightLevel;
            powerInputRight += lightScriptLeft.lightCoefficient * lightScriptLeft.lightLevel;
        }

        if (cruiseStraight && batteryPowered)
        {
            powerInputLeft += baseMotorTorque;
            powerInputRight += baseMotorTorque;
        }
        
        wheelLeft.Accelerate(powerInputLeft);
        wheelRight.Accelerate(powerInputRight);

        //Debug.Log("Light Intensity from Left: " + (lightScriptLeft.lightLevel));
        //Debug.Log("Light Intensity from Right: " + (lightScriptRight.lightLevel));
        //Debug.Log("Direction (how right): " + (powerInputLeft - powerInputRight));
        Debug.Log("Collided: " + (frontBumperScript.collided));
    }
}