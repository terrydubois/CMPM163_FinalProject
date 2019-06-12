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

/*
    public GameObject discsArr[0];
    public GameObject discsArr[1];
    public GameObject discsArr[2];
    public GameObject discsArr[3];
    public GameObject discsArr[4];
*/
    public void Update() {
        if (discSelected >= 0) {
            discSelectedObj.transform.position = discsArr[discSelected].transform.position;
        }
        else {
            discSelectedObj.transform.position = new Vector3(0, -3000, 0);
        }
    }

    public void discClickedCrash() {
        Debug.Log("crash clicked");
        discSelected = 0;
    }

    public void discClickedSpyro() {
        Debug.Log("spyro clicked");
        discSelected = 1;
    }

    public void discClickedFrogger() {
        Debug.Log("frogger clicked");
        discSelected = 2;
    }

    public void discClickedLSD() {
        Debug.Log("LSD clicked");
        discSelected = 3;
    }

    public void discClickedPetscop() {
        Debug.Log("petscop clicked");
        discSelected = 4;
    }

    public void mouseEnterCrash() {
        discHover.transform.position = new Vector3(
            discsArr[0].transform.position.x,
            discsArr[0].transform.position.y,
            0
        );
    }
    public void mouseEnterSpyro() {
        discHover.transform.position = new Vector3(
            discsArr[1].transform.position.x,
            discsArr[1].transform.position.y,
            0
        );
    }
    public void mouseEnterFrogger() {
        discHover.transform.position = new Vector3(
            discsArr[2].transform.position.x,
            discsArr[2].transform.position.y,
            0
        );
    }
    public void mouseEnterLSD() {
        discHover.transform.position = new Vector3(
            discsArr[3].transform.position.x,
            discsArr[3].transform.position.y,
            0
        );
    }
    public void mouseEnterPetscop() {
        discHover.transform.position = new Vector3(
            discsArr[4].transform.position.x,
            discsArr[4].transform.position.y,
            0
        );
    }

    public void mouseLeave() {
        discHover.transform.position = new Vector3(
            discHover.transform.position.x,
            3000,
            discHover.transform.position.z
        );
    }


}
