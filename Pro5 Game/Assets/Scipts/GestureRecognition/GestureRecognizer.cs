using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer
{
    private List<Point2D> points;

    private float angleTh;
    private float speedTh;
    private float cornerDist;
    private ReadInput RI;

    public GestureRecognizer(List <Point2D> Points, float AngleThreshold,
        float SpeedThreshold, float CornerDist, ReadInput RI)
    {
        points = Points;
        angleTh = AngleThreshold;
        speedTh = SpeedThreshold;
        cornerDist = CornerDist;
        this.RI = RI;
    }
    
    public int Recognize()
    {
        int nrOfPts = CountPoints();
        


        return nrOfPts;
    }


    public int CountPoints()    
    {

        List<Vector2> corners = new List<Vector2>();
        int nrOfPts = 2;
        float angle = 0;
        float speed = 0;
        float lastDist;

        Vector2 lastCorner = points[0].Pos;
        Vector2 p0;
        Vector2 p1;
        Vector2 p2;
        for(int i = 1; i < points.Count - cornerDist; i++)
        {
            p0 = points[i - 1].Pos;
            p1 = points[i].Pos;
            p2 = points[i + 1].Pos;
            

            /*speed = (p1 - p2).magnitude;
            /*
            if (speed > speedTh)
            {
                continue;
            }

            lastDist = (p1 - lastCorner).magnitude;

            if(lastDist < cornerDist)
            {
                continue;
            }
            */
            angle = Vector2.Angle(p0-p1, p2-p1);
            RI.Sout("Angle: " + angle.ToString());

            if (angle > angleTh)
            {
                continue;
            }
            corners.Add(p1);
            nrOfPts++;
            
            
        }
        RI.RenderPointsOI(corners);
        return nrOfPts;
    }
}
