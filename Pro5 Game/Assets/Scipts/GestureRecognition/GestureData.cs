using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureData
{
    public Gesture[] Gestures =
    {
        new Gesture(5,0.2f), //Quad
        new Gesture(4,0.2f), //Triangle
        new Gesture(2, 0.2f),//Circle
        new Gesture(2, 1f),  //Line
    };

    public int Match(Gesture other)
    {
        switch (other.nrOfCorners)
        {
            case 4:
                return 1;

            case 5:
                return 0;

            case 2:
                if(other.finalDist < 0.7f)
                {
                    return 2;
                }
                return 3;
        }
        return -1;
    }
}
