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

    public GestureRecognizer(List<Point2D> Points, float AngleThreshold,
        float SpeedThreshold, float CornerDist, ReadInput RI)
    {
        points = Points;
        angleTh = AngleThreshold;
        speedTh = SpeedThreshold;
        cornerDist = CornerDist;
        this.RI = RI;
    }

    public int Recognize(float TotalDistance)
    {
        int nrOfPts = CountPoints();
        float finalDist = (points[0].Pos - points[points.Count - 1].Pos).magnitude;
        finalDist /= TotalDistance;

        /*
         0: Triangle
         1: Triangle
         2: Circle
         3: Line
        */


        switch (nrOfPts)
        {
            case 4:
                return 0;

            case 5:
                return 1;

            case 2:
                if (finalDist < 0.7f)
                {
                    return 2;
                }
                return 3;
        }
        return -1;
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
        for (int i = 1; i < points.Count - 1; i++)
        {
            p0 = points[i - 1].Pos;
            p1 = points[i].Pos;
            p2 = points[i + 1].Pos;


            speed = (p1 - p2).magnitude;

            if (speed > speedTh)
            {
                continue;
            }

            lastDist = (p1 - lastCorner).magnitude;

            if (lastDist < cornerDist)
            {
                continue;
            }


            angle = Vector2.Angle(p0 - p1, p1 - p2);


            if (angle < angleTh)
            {
                continue;
            }
            corners.Add(p1);
            nrOfPts++;
            i += 3;


        }
        RI.RenderPointsOI(corners);
        return nrOfPts;
    }
}
