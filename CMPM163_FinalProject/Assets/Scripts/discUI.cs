using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class discUI : MonoBehaviour
{
    public int discSelected = -1;

    public GameObject discHover;
    public GameObject discSelectedObj;
    public GameObject[] discsArr;
    public GameObject discObj;
    public GameObject psx;

    public void Update() {
        if (discSelected >= 0) {
            discSelectedObj.transform.position = discsArr[discSelected].transform.position;
        }
        else {
            discSelectedObj.transform.position = new Vector3(0, -3000, 0);
        }
    }

    public void newDiscSequence1() {

        if (!psx.GetComponent<psxOpen>().lidOpen) {
            psx.GetComponent<psxOpen>().lidOpen = true;
            if (psx.GetComponent<psxOpen>().discIn) {
                Invoke("newDiscSequence2", 0.5f);
            }
            else {
                Invoke("newDiscSequence3", 1);
            }
        }
    }

    public void newDiscSequence2() {
        psx.GetComponent<psxOpen>().discIn = false;
        Invoke("newDiscSequence3", 2f);
    }

    public void newDiscSequence3() {
        psx.GetComponent<psxOpen>().discIn = true;
        Invoke("newDiscSequence4", 1);
    }

    public void newDiscSequence4() {
        psx.GetComponent<psxOpen>().lidOpen = false;
    }

    public void discClicked(int discIndex) {
        if (discSelected != discIndex && !psx.GetComponent<psxOpen>().lidOpen) {
            Debug.Log("disc clicked: " + discIndex);
            discSelected = discIndex;
            newDiscSequence1();
        }
    }

    public void mouseEnter(int discIndex) {
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
