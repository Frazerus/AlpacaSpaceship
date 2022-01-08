using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{

    public GameObject dotPrefab;
    public GameObject IMPrefab;

    public float AngleThreshold = 10;
    public float SpeedThresholdModifier = 1;
    public float cornerDivider = 50;
    public float distThreshold = 1;

    private bool started = false;
    private bool ended = false;
    private bool useMouse = true;

    private float SpeedThreshold;
    private float totalMovement = 0;

    private int countGestures = 0;
    private Touch touch;

    private List<Point2D> points;
    private Vector2 startPoint;

    void Start()
    {

    }

    void Update()
    {
        if (RegisteredInput())
        {
            if (!started)
            {
                StartRecording();
            }
            else if (ended)
            {
                EndRecording();
            }
            else
            {
                ContinueRecording();
            }
        }
        else if (started)
        {
            EndRecording();
        }
    }

    private void StartRecording()
    {
        started = true;
        if (useMouse)
        {
            startPoint = Input.mousePosition;
        }

        //TODO ADD TOUCH INPUT

        points = new List<Point2D>();
        points.Add(new Point2D(startPoint, 0));
    }

    private void EndRecording()
    {
        foreach (var point in points)
        {
            print(point);
        }

        SpeedThreshold = (totalMovement / points.Count) * SpeedThresholdModifier;
        float CornerDist = totalMovement / points.Count * cornerDivider;

        int Gesture = new GestureRecognizer(points, AngleThreshold, SpeedThreshold, CornerDist, this).Recognize();

        foreach (var point in points)
        {
            GameObject go = Instantiate(dotPrefab);
            go.transform.position = point.Pos;
        }


        print("SpeedTH: " + SpeedThreshold);
        print("CornerDist: " + CornerDist);

        print("Nr Gesture Points: " + Gesture);
        print("Nr of Points total: " + points.Count);
        print("Total Dist Travelled: " + totalMovement);

        ResetVariables();

    }

    private void ContinueRecording()
    {
        Vector2 newPoint;
        if (useMouse)
        {
            newPoint = Input.mousePosition;
        }
        else
        {
            newPoint = new Vector2(0, 0);
        }


        Vector2 dist = newPoint - points[points.Count - 1].Pos;
        if (dist.magnitude > distThreshold)
        {
            totalMovement += dist.magnitude;
            points.Add(new Point2D(newPoint, dist.magnitude));
        }
    }

    private bool RegisteredInput()
    {
        if (Input.touchCount > countGestures) {
            countGestures++;
            useMouse = false;

            return true;
        }
        return Input.GetMouseButton(0);
    }

    public void RenderPointsOI(List<Vector2> points)
    {
        foreach (var point in points)
        {
            GameObject go = Instantiate(IMPrefab);
            go.transform.position = point;
        }
    }

    private void ResetVariables()
    {
        started = false;
        ended = false;
        useMouse = true;
        totalMovement = 0;
    }

    public void Sout(string sout){
        print(sout);
    }
}
