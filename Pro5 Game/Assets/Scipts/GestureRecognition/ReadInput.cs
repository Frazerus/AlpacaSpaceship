using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReadInput : MonoBehaviour
{

    public GameObject dotPrefab;
    public GameObject IMPrefab;
    public GameObject Player;
    public GameObject LR;

    public float AngleThreshold = 10;
    public float SpeedThresholdModifier = 1;
    public float cornerDivider = 50;
    public float distThreshold = 1;
    public int maxPointsDrawn;
    public float lineFadeOutTime = 0.5f;
    public float startWidth = 0.010f;
    public float endWidth = 0.20f;

    public bool DEBUG = true;

    private bool started = false;
    private bool ended = false;
    private bool useMouse = true;

    private float SpeedThreshold;
    private float totalMovement = 0;

    private int lineCount = 0;

    private Touch touch;

    
    private List<Point2D> points;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 previousPoint;
    private Player PlayerScript;
    private LineRenderer lineRenderer;
    private Camera cam;

    
    private Vector3[] lrPoints;
    private int nrArrayPoints = 0;
    private float SecsPerFadePos;
    private float dTime = 0;
    


    void Start()
    {
        
        PlayerScript = Player.GetComponent<Player>();
        lineRenderer = LR.GetComponent<LineRenderer>();
        
        
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.positionCount = maxPointsDrawn;

        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] { 
                new GradientColorKey(lineRenderer.startColor, 0.0f), 
                new GradientColorKey(lineRenderer.endColor, 1.0f)},
            new GradientAlphaKey[] { 
                new GradientAlphaKey(0.0f, 1.0f), 
                new GradientAlphaKey(1.0f, 1.0f)});
        lineRenderer.colorGradient = gradient;

        cam = Camera.main;
        lrPoints = new Vector3[maxPointsDrawn];

        SecsPerFadePos = lineFadeOutTime / maxPointsDrawn;



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
        else if (lineRenderer.positionCount > 0)
        {
            UpdateDestroyLineRenderer();

        }

    }

    private void StartRecording()
    {


        lrPoints = new Vector3[maxPointsDrawn];

        started = true;
        if (useMouse)
        {
            startPoint = Input.mousePosition;
        }
        else
        {
            startPoint = Input.GetTouch(0).position;
            
        }
        previousPoint = startPoint;

        /*points = new List<Point2D>();
        points.Add(new Point2D(startPoint, 0));
        */
        DrawLine(startPoint);

        
    }

    private void EndRecording()
    {


        /*SpeedThreshold = (totalMovement / points.Count) * SpeedThresholdModifier;
        float CornerDist = totalMovement / points.Count * cornerDivider;

        int Gesture = new GestureRecognizer(points, AngleThreshold, SpeedThreshold, CornerDist, this).Recognize(totalMovement);
        if (DEBUG)
        {

            foreach (var point in points)
            {
                GameObject go = Instantiate(dotPrefab);
                go.transform.position = point.Pos;
            }

            foreach (var point in points)
                    {
                        print(point);
                    }

            print("SpeedTH: " + SpeedThreshold);
            print("CornerDist: " + CornerDist);

            print("Nr Gesture Points: " + Gesture);
            print("Nr of Points total: " + points.Count);
            print("Total Dist Travelled: " + totalMovement);
        }
        */

        if (useMouse)
        {
            endPoint = previousPoint;
        }
        else
        {
            endPoint = previousPoint;
        }


        int Gesture = SimpleGestureRecognition.Recognize(totalMovement, startPoint, endPoint);
        PlayerScript.attack(Gesture);
        ResetVariables();

    }

    private void ContinueRecording()
    {


        Vector2 newPoint;
        if (useMouse)
        {
            newPoint = Input.mousePosition;
            DrawLine(Input.mousePosition);
        }
        else
        {
            touch = Input.GetTouch(0);
            DrawLine(touch.position);
            newPoint = touch.position;
        }



        Vector2 dist = newPoint - previousPoint;
        if (dist.magnitude > distThreshold)
        {
            totalMovement += dist.magnitude;
            //points.Add(new Point2D(newPoint, dist.magnitude));
        }
        previousPoint = newPoint;

        

        
        
    }

    private void DrawLine(Vector3 pos)
    {

        pos.z = cam.nearClipPlane + 1;
        pos= cam.ScreenToWorldPoint(pos);

        if(nrArrayPoints < maxPointsDrawn)
        {
            lineRenderer.positionCount = nrArrayPoints;
            lrPoints[nrArrayPoints] = pos;
            lineRenderer.SetPositions(lrPoints);
            nrArrayPoints++;
        }
        else
        {

            ShiftLineRendererArray();

            lrPoints[maxPointsDrawn - 1] = pos;


            lineRenderer.positionCount = maxPointsDrawn;
            lineRenderer.SetPositions(lrPoints);
        }

    }

    private void UpdateDestroyLineRenderer()
    {
        dTime += Time.deltaTime;
        if(dTime > SecsPerFadePos)
        {
            ShiftLineRendererArray();
            lineRenderer.SetPositions(lrPoints);
            lineRenderer.positionCount--;
            dTime -= SecsPerFadePos;
        }
        
    }

    private bool RegisteredInput()
    {
        if (Input.touchCount > 0) {
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
        nrArrayPoints = 0;
    }

    private void ShiftLineRendererArray()
    {
        for (int i = 0; i < maxPointsDrawn - 1; i++)
        {
            lrPoints[i] = lrPoints[i + 1];
        }
    }

}
