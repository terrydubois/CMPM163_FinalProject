using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psxOpen : MonoBehaviour
{
    public bool psxRot = true;
    public Vector3 psxRotAmount;
    private float psxCurrentXRot = 0;
    private float psxCurrentYRot = 0;
    private float psxCurrentZRot = 0;

    public bool lidOpen = false;
    public float lidClosedXRot;
    public float lidOpenXRot;
    private float lidCurrentXRot;

    public GameObject lid;

    void Start()
    {
        lidCurrentXRot = lidClosedXRot;
    }

    void Update()
    {
        // make PSX spin/float around
        if (psxRot) {
            lidOpen = false;
            transform.Rotate(psxRotAmount * Time.deltaTime);
            psxCurrentXRot = transform.eulerAngles.x;
            psxCurrentYRot = transform.eulerAngles.y;
            psxCurrentZRot = transform.eulerAngles.z;
        }
        else {
            psxCurrentXRot = Approach(psxCurrentXRot, 0, 8);
            psxCurrentYRot = Approach(psxCurrentYRot, 0, 8);
            psxCurrentZRot = Approach(psxCurrentZRot, 0, 8);
            transform.rotation = Quaternion.Euler(psxCurrentXRot, psxCurrentYRot, psxCurrentZRot);
        }


        // open or close lid of PSX
        var currentXRotDest = (lidOpen) ? lidOpenXRot : lidClosedXRot;
        lidCurrentXRot = Approach(lidCurrentXRot, currentXRotDest, 12);
        lid.transform.rotation = Quaternion.Euler(lidCurrentXRot + transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        //lid.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
    }


    // smoothly transition a value to a desired value
    float Approach(float value, float valueDest, float divisor)
    {
        if (value < valueDest) {
            value += Mathf.Abs(value - valueDest) / divisor;
        }
        else if (value > valueDest) {
            value -= Mathf.Abs(value - valueDest) / divisor;
        }

        return value;
    }
}
