using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{

    public float cornerThreshold;


    private bool started = false;
    private bool ended = false;
    private bool useMouse = true;

    private int countGestures = 0;
    private Touch touch;

    private List<Vector2> points;
    private Vector2 startPoint;

    private int nrPoints = 0;



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

        points = new List<Vector2>();
        points.Add(startPoint);
    }

    private void EndRecording()
    {
        started = false;
        foreach ( var point in points)
        {
            print(point);
        }
        CountPoints();
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

        points.Add(newPoint);
    }

    private bool RegisteredInput()
    {
        if (Input.touchCount>countGestures){
            countGestures++;
            useMouse = false;
            
            return true;
        }
        return Input.GetMouseButton(0);
    }

    private void CountPoints()
    {
         
    }

    

    
}
