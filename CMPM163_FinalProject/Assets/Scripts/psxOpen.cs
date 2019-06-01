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
    public GameObject cam;

    void Start()
    {
        lidCurrentXRot = lidClosedXRot;
    }

    void Update()
    {
        if (psxRot) {
            // rotate PSX around all floaty
            lidOpen = false;
            transform.Rotate(psxRotAmount * Time.deltaTime);
            psxCurrentXRot = transform.eulerAngles.x;
            psxCurrentYRot = transform.eulerAngles.y;
            psxCurrentZRot = transform.eulerAngles.z;
        }
        else {
            // return to original rotation
            psxCurrentXRot = ApproachSmooth(psxCurrentXRot, 0, 8);
            psxCurrentYRot = ApproachSmooth(psxCurrentYRot, 0, 8);
            psxCurrentZRot = ApproachSmooth(psxCurrentZRot, 0, 8);
            transform.rotation = Quaternion.Euler(psxCurrentXRot, psxCurrentYRot, psxCurrentZRot);
        }

        // open or close lid of PSX
        var currentXRotDest = (lidOpen) ? lidOpenXRot : lidClosedXRot;
        lidCurrentXRot = ApproachSmooth(lidCurrentXRot, currentXRotDest, 12);
        lid.transform.rotation = Quaternion.Euler(lidCurrentXRot + transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);


        // if camera is pulled back and the player clicks, orient the console and open the lid
        if (cam.GetComponent<openingSequence>().camSequenceOver) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("here");
                if (psxRot) {
                    psxRot = false;
                }
                else {
                    lidOpen = !lidOpen;
                }
            }
        }
    }


    // smoothly transition a value to a desired value
    float ApproachSmooth(float value, float valueDest, float divisor)
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
