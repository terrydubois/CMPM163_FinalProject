using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psxOpen : MonoBehaviour
{
    public bool psxRot = true;
    public Vector3 psxRotAmount;

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

        }


        // lidOpen or close lid of PSX
        var currentXRotDest = (lidOpen) ? lidOpenXRot : lidClosedXRot;
        lidCurrentXRot = Approach(lidCurrentXRot, currentXRotDest, 12);
        lid.transform.rotation = Quaternion.Euler(lidCurrentXRot, 0, 0);
    }

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
