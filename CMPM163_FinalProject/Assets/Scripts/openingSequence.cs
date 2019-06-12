using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class openingSequence : MonoBehaviour
{
    public GameObject cam;
    public GameObject screen;
    public Light ceilingLight;
    private float lightIntensityMin = 0;

    public bool camSequenceOver = false;
    public float yStart;
    public float zStart;
    public float yEnd;
    public float zEnd;
    public float camPanSpeedDest = 0.04f;
    private float camPanSpeed = 0;
    public float holdTimer;

    public float discsYStart;
    public float discsYEnd;

    public VideoPlayer[] vids;
    public AudioSource[] ambientAudio;
    public GameObject discs;
    public GameObject psx;

    void Start()
    {
        transform.position = new Vector3(0, yStart, zStart);
        ceilingLight.intensity = 0;

        vids = screen.GetComponents<VideoPlayer>();

        // set discs to default position
        discs.transform.position = new Vector3(
            discs.transform.position.x,
            discsYStart,
            discs.transform.position.z
        );
    }

    void Update()
    {
        
        if (holdTimer <= 0) {
            camPanSpeed = Approach(camPanSpeed, camPanSpeedDest, camPanSpeedDest / 200);
            var newY = Approach(transform.position.y, yEnd, camPanSpeed);
            var newZ = Approach(transform.position.z, zEnd, camPanSpeed);
            transform.position = new Vector3(0, newY, newZ);

            vids[0].enabled = false;
            vids[1].enabled = true;


            if (ambientAudio[0].volume < 1) {
                ambientAudio[0].volume += 0.001f;
            }
            else {
                ambientAudio[0].volume = 1;
            }
            ambientAudio[1].volume = ambientAudio[0].volume;
        }
        else {
            holdTimer--;
            vids[0].enabled = true;
            vids[1].enabled = false;

            ambientAudio[0].volume = 0.2f;
            ambientAudio[1].volume = 0.2f;
        }

        // slowly bring in light
        if (Mathf.Abs(transform.position.z - zEnd) < (Mathf.Abs(zStart - zEnd) * 0.9f)) {
            // flicker light
            ceilingLight.intensity += Random.Range(-0.06f, 0.06f);
            lightIntensityMin += 0.005f;
            lightIntensityMin = Mathf.Min(lightIntensityMin, 1.0f);
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

        // if camera is panned out, bring disc UI in
        float discsYDest = discsYStart;
        if (camSequenceOver && !psx.GetComponent<psxOpen>().psxRot) {
            discsYDest = discsYEnd;
        }
        var discsYCurrent = Approach(discs.transform.position.y, discsYDest, 3);
        discs.transform.position = new Vector3(
            discs.transform.position.x,
            discsYCurrent,
            discs.transform.position.z
        );
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