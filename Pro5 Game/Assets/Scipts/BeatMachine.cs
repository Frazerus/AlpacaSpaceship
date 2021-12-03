using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatMachine : MonoBehaviour
{
    public static BeatMachine current;
    public double beatSec;
    public double beatOffset = 2;

    [SerializeField] private int bPm = 128;
    [SerializeField] private bool doTick;
    [SerializeField] private bool showBeat;
    [SerializeField] private Text debugOutput;

    private AudioSource tick;
    private double deltaSec;
    private double deltaSecOff;
    private int numBeat;
    private bool started = true;
   
    private int takt;
    
    private void Awake()
    {
        current = this;
        tick = GetComponent<AudioSource>();
        current.onBegin += startPlaying;
        current.onEnd += End;

        beatSec = 1 / ((float)bPm / 60);
        deltaSec = 0;
        deltaSecOff = beatSec * beatOffset;
    }


    // Update is called once per frame
    void Update()
    {
        if (started)
        {

            beatSec = 1 / ((double)bPm / 60);
            deltaSec += Time.deltaTime;
            deltaSecOff += Time.deltaTime;
        
            //print(beatSec + " | " + deltaSec);
        
            if(deltaSecOff >= beatSec)
            {
                deltaSecOff = deltaSecOff - beatSec;
                current.OffBeat();

                if (doTick)
                {
                    if (takt == 0)
                    {
                        tick.pitch = 1.2f;
                        tick.Play();
                        takt++;
                    }
                    else
                    {
                        tick.pitch = 0.8f;
                        tick.Play();
                        takt = (takt < 3) ? takt + 1 : 0;
                    }

                }
            }


            if (deltaSec >= beatSec)
            {
                current.Beat();
                
                //print("****************************************" + deltaSec);
                deltaSec = deltaSec - beatSec;
                
                numBeat++;
                debugOutput.text = numBeat.ToString();
                if (showBeat)
                {
                
                }
                
            }
        }
    }

    public void startPlaying()
    {
        started = true;
        Update();
    }

    public float createRating()
    {
        
        double max = deltaSec/beatSec;

        //print(deltaSec + " | " + beatSec);
        max -= 0.5;
        max = max >= 0 ? max : -max;

      

        float rating = (float)max * 2;
        
        // Placeholder output of the rating of the last attack
        print("Rating: " + rating);

        return rating;
    }

    public void createRatingAndSend()
    {
        Rating(createRating());
    }

    private void End()
    {
        started = false;
    }

    //Happens every beat once, at the divided time
    public event Action onOffBeat;

    public void OffBeat()
    {
        if (onOffBeat != null)
        {
            onOffBeat();
        }
    }
    
    //Happens every Beat
    public event Action onBeat;

    public void Beat()
    {
        if (onBeat != null)
        {
            onBeat();
        }
    }


    //Happens when the player attacks using input
    public event Action<int> onAttack;

    public void Attack(int type)
    {
        if (onAttack != null)
        {
            onAttack(type);
        }
    }



    //Happens when an Enemy gets killed
    public event Action<GameObject> onKilled;

    public void Killed(GameObject obj)
    {
        if (onKilled != null)
        {
            onKilled(obj);
        }
    }

    public event Action<float> onRating;

    public void Rating(float rating)
    {
        if(onRating != null)
        {
            onRating(rating);
        }
    }


    public event Action onBegin;
    public void Begin()
    {
        if(onBegin != null)
        {
            onBegin();
        }
    }

    public event Action onEnd;

    public void EndPlaying()
    {
        if(onEnd != null)
        {
            onEnd();
        }
    }



    
}
