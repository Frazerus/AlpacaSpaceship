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
}
