using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    public GameObject[] cube;
    private MeshRenderer[] cuberender = new MeshRenderer[6];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            cuberender[i] = cube[i].GetComponent<MeshRenderer>();
            cuberender[i].material.color = Color.white;
        }     
    }


    public void ColorChange()
    { 
        for (int i = 0; i < 3; i++)
        {
            if (cuberender[i].material.color == Color.white)
                cuberender[i].material.color = Color.red;
            else
                cuberender[i].material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
