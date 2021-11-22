using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTest : MonoBehaviour

{
    private GameObject test;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        BeatMaschine.current.onBeat += Beat;
        test = GameObject.Find("BeatTestCube");
        material = test.GetComponent<MeshRenderer>().material;
        
    }
    
    public void Beat()
    {
        print("Beat");
        if (material.color == Color.blue)
        {
            material.SetColor("_Color", Color.red);
        }
        else
        {
            print("Blue Beat");
            material.SetColor("_Color", Color.blue);
        }
    }
}
