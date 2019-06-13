using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideDiscs : MonoBehaviour
{
    public bool hide;
    public GameObject discs;
    public float discsYStart;
    public float discsYEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float discsYDest = discsYEnd;
        if (hide)
        {
            discsYDest = discsYStart;
        }
        var discsYCurrent = Approach(discs.transform.position.y, discsYDest, 3);
        discs.transform.position = new Vector3(
            discs.transform.position.x,
            discsYCurrent,
            discs.transform.position.z

        );
    }
    public void toggleHide()
    {
        hide = !hide;
    }

    // transition a value to a desired value by linear speed
    float Approach(float value, float valueDest, float speed)
    {
        if (Mathf.Abs(value - valueDest) < speed)
        {
            return valueDest;
        }
        if (value < valueDest)
        {
            return value += speed;
        }
        else if (value > valueDest)
        {
            return value -= speed;
        }
        return value;
    }

}
