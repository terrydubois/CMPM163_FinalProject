using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psxOpen : MonoBehaviour
{
    public bool open = false;
    public float closedXRot;
    public float openXRot;
    private float currentXRot;

    public GameObject lid;

    void Start()
    {
        currentXRot = closedXRot;
    }

    void Update()
    {
        var currentXRotDest = (open) ? openXRot : closedXRot;
        currentXRot = Approach(currentXRot, currentXRotDest, 12);

        lid.transform.rotation = Quaternion.Euler(currentXRot, 0, 0);
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
