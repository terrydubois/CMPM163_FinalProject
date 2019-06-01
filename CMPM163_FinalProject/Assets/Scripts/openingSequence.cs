using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingSequence : MonoBehaviour
{
    public GameObject cam;
    public Light ceilingLight;
    private float lightIntensityMin = 0;

    public bool camSequenceOver = false;
    public float yStart;
    public float zStart;
    public float yEnd;
    public float zEnd;
    public float camPanSpeed = 0.04f;
    public float holdTimer;

    void Start()
    {
        transform.position = new Vector3(0, yStart, zStart);
        ceilingLight.intensity = 0;
    }

    void Update()
    {
        if (holdTimer <= 0) {
            var newY = Approach(transform.position.y, yEnd, camPanSpeed);
            var newZ = Approach(transform.position.z, zEnd, camPanSpeed);
            transform.position = new Vector3(0, newY, newZ);
        }
        else {
            holdTimer--;
        }

        // slowly bring in light
        if (Mathf.Abs(transform.position.z - zEnd) < (Mathf.Abs(zStart - zEnd) / 2)) {
            // flicker light
            ceilingLight.intensity += Random.Range(-0.03f, 0.03f);
            lightIntensityMin += 0.01f;
            lightIntensityMin = Mathf.Min(lightIntensityMin, 0.75f);
        }
        ceilingLight.intensity = Mathf.Clamp(ceilingLight.intensity, lightIntensityMin, 1.5f);

        // cam sequence is over if camera is close enough to its final destination
        camSequenceOver = (Mathf.Abs(transform.position.y - yEnd) < 0.5f
                        && Mathf.Abs(transform.position.z - zEnd) < 0.5f);

        // fast-forward the cam sequence by pressing Q                        
        if (Input.GetKeyDown(KeyCode.Q)) {
            holdTimer = 0;
            camPanSpeed = 1;
        }
    }
    
    // transition a value to a desired value by linear speed
    float Approach(float value, float valueDest, float speed)
    {
        if (Mathf.Abs(value - valueDest) < speed) {
            return valueDest;
        }
        if (value < valueDest) {
            return value += speed;
        }
        else if (value > valueDest) {
            return value -= speed;
        }
        return value;
    }
}