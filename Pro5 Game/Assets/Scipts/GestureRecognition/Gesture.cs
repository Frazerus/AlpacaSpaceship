using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture
{
    public int nrOfCorners
    { 
        get; 
    }
        
    public float finalDist
    {
        get;
    }

    public Gesture(int NrOfCorners,  float FinalDist)
    {
        nrOfCorners = NrOfCorners;
        finalDist = FinalDist;
    }
    
}
