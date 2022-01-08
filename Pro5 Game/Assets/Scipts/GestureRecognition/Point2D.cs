using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2D
{
    public Point2D(Vector2 position, float spd)
    {
        Pos = position;
        Speed = spd;
    }

    public Vector2 Pos
    {
        get;
    }

    public float Speed
    {
        get;
    }

    public override string ToString()
    {
        return ("Pos: " + Pos.ToString() + "::: Speed: " + Speed);
    }
    
}
