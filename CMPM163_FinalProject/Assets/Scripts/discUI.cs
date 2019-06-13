﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class discUI : MonoBehaviour
{
    public int discSelected = -1;

    public GameObject discHover;
    public GameObject discSelectedObj;
    public GameObject[] discsArr;
    public GameObject discObj;
    public GameObject psx;
    public GameObject psx_renderer;
    public GameObject cam;
    public GameObject screen;
    public GameObject cover;
    public GameObject cover_renderer;
    public VideoClip[] vidClips;
    public VideoPlayer[] vids;
    public Sprite[] covers;
    public AudioSource[] audioClips;
    public Material[] modelShaders;
    public GameObject[] fireParticles;

    public float[] discsArrRotSpeed;
    public float[] discsArrRotSpeedDest;


    public bool newDiscSequenceInProgress;

    public void Start() {
        vids = screen.GetComponents<VideoPlayer>();
        newDiscSequenceInProgress = false;
        audioClips = GetComponents<AudioSource>();

        discHover.transform.position = new Vector3(0, -2000, 0);
    }

    public void Update() {
        if (discSelected >= 0) {
            discSelectedObj.transform.position = discsArr[discSelected].transform.position;
        }
        else {
            discSelectedObj.transform.position = new Vector3(0, -3000, 0);
        }
        var changePos = Mathf.Abs(36 - discObj.transform.position.y);
        if (changePos < 1)
        {
            if (discSelected >= 0)
            {
                cover.GetComponent<SpriteRenderer>().sprite = covers[discSelected];
            }
        }

        for (var i = 0; i < discsArrRotSpeed.Length; i++) {
            if (discsArrRotSpeed[i] < discsArrRotSpeedDest[i]) {
                discsArrRotSpeed[i] = ApproachSmooth(discsArrRotSpeed[i], discsArrRotSpeedDest[i], 40);
            }
            else if (discsArrRotSpeed[i] > discsArrRotSpeedDest[i]) {
                discsArrRotSpeed[i] = ApproachSmooth(discsArrRotSpeed[i], discsArrRotSpeedDest[i], 12);
            }
            discsArr[i].transform.Rotate(new Vector3(0, 0, discsArrRotSpeed[i] * Time.deltaTime));
        }
    }

    public void newDiscSequence1() {
        // the starting animation of putting the disc in the console
        if (!psx.GetComponent<psxOpen>().lidOpen) {
            psx.GetComponent<psxOpen>().lidOpen = true;
            if (psx.GetComponent<psxOpen>().discIn) {
                Invoke("newDiscSequence2", 0.5f);
                for (var i = 0; i < discsArrRotSpeed.Length; i++) {
                    discsArrRotSpeedDest[i] = 0;
                }
            }
            else {
                Invoke("newDiscSequence3", 1);
            }
            vids[1].clip = vidClips[0];
            audioClips[0].Play();
            audioClips[1].Stop();
        }
    }

    // Take out previous disk
    public void newDiscSequence2() {
        psx.GetComponent<psxOpen>().discIn = false;
        cover_renderer.GetComponent<Renderer>().material = modelShaders[0];
        psx_renderer.GetComponent<Renderer>().material = modelShaders[0];
        particleChange(false);
        Invoke("newDiscSequence3", 2f);
    }

    // New disk has gotten into disk slot
    public void newDiscSequence3() {
        psx.GetComponent<psxOpen>().discIn = true;
        Invoke("newDiscSequence4", 1.5f);
    }

    // Lid closing has begun
    public void newDiscSequence4() {
        psx.GetComponent<psxOpen>().lidOpen = false;
        Invoke("newDiscSequence5", 1);
        audioClips[1].Play();
    }

    // Lid is closed, video is starting
    public void newDiscSequence5() {
        vids[1].clip = vidClips[discSelected + 1];
        cover_renderer.GetComponent<Renderer>().material = modelShaders[discSelected + 1];
        psx_renderer.GetComponent<Renderer>().material = modelShaders[discSelected + 1];
        if ((discSelected + 1) == 2)
        {
            particleChange(true);
        }
        newDiscSequenceInProgress = false;
        for (var i = 0; i < discsArrRotSpeed.Length; i++) {
            discsArrRotSpeedDest[i] = (discSelected == i) ? 2000 : 0;
        }
    }

    public void discClicked(int discIndex) {
        // click on disc
        if (discSelected != discIndex && !psx.GetComponent<psxOpen>().lidOpen
        && !newDiscSequenceInProgress
        && cam.GetComponent<openingSequence>().camSequenceOver) {
            Debug.Log("disc clicked: " + discIndex);
            discSelected = discIndex;
            newDiscSequence1();
            newDiscSequenceInProgress = true;
        }
    }

    public void mouseEnter(int discIndex) {
        // mouseover disc
        if (!psx.GetComponent<psxOpen>().lidOpen) {
            discHover.transform.position = new Vector3(
                discsArr[discIndex].transform.position.x,
                discsArr[discIndex].transform.position.y,
                0
            );
        }
        else {
            mouseLeave();
        }
    }

    // WHich disk has been lciked by the user
    public void discClickedCrash() {
        discClicked(0);
    }
    public void discClickedSpyro() {
        discClicked(1);
    }
    public void discClickedFrogger() {
        discClicked(2);
    }
    public void discClickedLSD() {
        discClicked(3);
    }
    public void discClickedPetscop() {
        discClicked(4);
    }

    // Which game is the mouse hovering over
    public void mouseEnterCrash() {
        mouseEnter(0);
    }
    public void mouseEnterSpyro() {
        mouseEnter(1);
    }
    public void mouseEnterFrogger() {
        mouseEnter(2);
    }
    public void mouseEnterLSD() {
        mouseEnter(3);
    }
    public void mouseEnterPetscop() {
        mouseEnter(4);
    }

    public void mouseLeave() {
        discHover.transform.position = new Vector3(
            discHover.transform.position.x,
            3000,
            discHover.transform.position.z
        );
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

    private void particleChange(bool active)
    {
        
        //{
            for (int i = 0; i < fireParticles.Length; i++)
            {
                if (fireParticles[i].activeInHierarchy != active)
                {
                    fireParticles[i].SetActive(active);
                }
            }
       // }
       /* else
        {
            Debug.Log("else");
            for (int i = 0; i < fireParticles.Length; i++)
            {
                if (!fireParticles[i].activeInHierarchy)
                {
                    fireParticles[i].SetActive(true);
                }
            }
        }*/
    }


}
