using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class discUI : MonoBehaviour
{
    public int discSelected = -1;

    public GameObject discHover;
    public GameObject discCrash;
    public GameObject discSpyro;
    public GameObject discFrogger;
    public GameObject discLSD;
    public GameObject discPetscop;

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
            discCrash.transform.position.x,
            discCrash.transform.position.y,
            0
        );
    }
    public void mouseEnterSpyro() {
        discHover.transform.position = new Vector3(
            discSpyro.transform.position.x,
            discSpyro.transform.position.y,
            0
        );
    }
    public void mouseEnterFrogger() {
        discHover.transform.position = new Vector3(
            discFrogger.transform.position.x,
            discFrogger.transform.position.y,
            0
        );
    }
    public void mouseEnterLSD() {
        discHover.transform.position = new Vector3(
            discLSD.transform.position.x,
            discLSD.transform.position.y,
            0
        );
    }
    public void mouseEnterPetscop() {
        discHover.transform.position = new Vector3(
            discPetscop.transform.position.x,
            discPetscop.transform.position.y,
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
