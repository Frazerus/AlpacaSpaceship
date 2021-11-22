using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatTest : MonoBehaviour

{
    [SerializeField] private Color c1 = Color.red;
    [SerializeField] private Color c2 = Color.gray;
    
    private GameObject test;
    private Image material;
    // Start is called before the first frame update
    void Start()
    {
        BeatMachine.current.onBeat += Beat;
        material = gameObject.GetComponentInParent<Image>();
        
    }
    
    public void Beat()
    {
        if (material.color == c2)
        {
            material.color = c1;
        }
        else
        {
            material.color = c2;
        }
    }
}
