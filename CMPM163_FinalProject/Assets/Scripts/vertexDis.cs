using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vertexDis : MonoBehaviour
{
    Renderer render;
    int amountDest = 5;
    int amountDefault = 0;
    int currentAmount = 0;
    bool isbig = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();

        render.material.shader = Shader.Find("Custom/VertexDis");


    }

    // Update is called once per frame
    void Update()
    {

        if (currentAmount < 5 && !isbig)
        {
            currentAmount += 1;
            render.material.SetFloat("_Amount", currentAmount);
        }
        if(currentAmount == 5)
        {
            isbig = true;
        }
        if(currentAmount > 0 && isbig) {
            currentAmount -= 1;
            render.material.SetFloat("_Amount", currentAmount);
        }
            if(currentAmount == 0)
        {
            isbig = false;
        }

    }
}
