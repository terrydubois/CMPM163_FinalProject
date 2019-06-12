using System.Collections;
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
    public GameObject cam;
    public GameObject screen;
    public GameObject cover;
    public VideoClip[] vidClips;
    public VideoPlayer[] vids;
    public Sprite[] covers;


    public bool newDiscSequenceInProgress;

    public void Start() {
        vids = screen.GetComponents<VideoPlayer>();
        newDiscSequenceInProgress = false;
    }

    public void Update() {
        if (discSelected >= 0) {
            discSelectedObj.transform.position = discsArr[discSelected].transform.position;
        }
        else {
            discSelectedObj.transform.position = new Vector3(0, -3000, 0);
        }
        var changePos = Mathf.Abs(36 - discObj.transform.position.y);
        Debug.Log(changePos);
        if (changePos < 1)
        {
            Debug.Log("covertime");
            if (discSelected >= 0)
            {
                cover.GetComponent<SpriteRenderer>().sprite = covers[discSelected];
            }
        }
    }

    public void newDiscSequence1() {
        // the starting animation of putting the disc in the console
        if (!psx.GetComponent<psxOpen>().lidOpen) {
            psx.GetComponent<psxOpen>().lidOpen = true;
            if (psx.GetComponent<psxOpen>().discIn) {
                Invoke("newDiscSequence2", 0.5f);
            }
            else {
                Invoke("newDiscSequence3", 1);
            }
            vids[1].clip = vidClips[0];
        }
    }

    public void newDiscSequence2() {
        psx.GetComponent<psxOpen>().discIn = false;
        Invoke("newDiscSequence3", 2f);
    }

    public void newDiscSequence3() {
        psx.GetComponent<psxOpen>().discIn = true;
        Invoke("newDiscSequence4", 1.5f);
    }

    public void newDiscSequence4() {
        psx.GetComponent<psxOpen>().lidOpen = false;
        Invoke("newDiscSequence5", 1);
    }

    public void newDiscSequence5() {
        vids[1].clip = vidClips[discSelected + 1];
        newDiscSequenceInProgress = false;
    }

    public void discClicked(int discIndex) {
        // click on disc
        if (discSelected != discIndex && !psx.GetComponent<psxOpen>().lidOpen
        && !newDiscSequenceInProgress) {
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


}
