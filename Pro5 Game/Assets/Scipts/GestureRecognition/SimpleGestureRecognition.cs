using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGestureRecognition
{
    
    public static int Recognize(float TotalDistance, Vector2 pos1, Vector2 pos2)
    {
        float finalDist = (pos1 - pos2).magnitude;
        finalDist /= TotalDistance;

        if (finalDist < 0.7f )
        {
            return 2;
        }


        Vector2 diff = pos2 - pos1;

        if (diff.x * diff.y > 0)
        {
            return 1;
        }

        return 0;
    }
}
