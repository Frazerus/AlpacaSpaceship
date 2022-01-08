using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture
{
    int nrOfCorners;
    float finalDist;

    public Gesture(int NrOfCorners,  float FinalDist)
    {
        nrOfCorners = NrOfCorners;
        finalDist = FinalDist;
    }


    public float Match(Gesture other)
    {
        return finalDist - other.finalDist;
    }

    public bool CloseMatch(Gesture other)
    {
        return other.nrOfCorners == nrOfCorners;
    }
}
