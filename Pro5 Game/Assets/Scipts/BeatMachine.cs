using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMachine : MonoBehaviour
{
    public static BeatMachine current;
    public double beatSec;

    [SerializeField] private int bPm = 128;
    [SerializeField] private bool doTick;
    [SerializeField] private int beatDivider = 2;

    private AudioSource tick;
    private double deltaSec;
    private double deltaSecOff;
   
    private int takt;
    
    private void Awake()
    {
        current = this;
        tick = GetComponent<AudioSource>();
        current.onKilled += EnemyKilled;

        beatSec = 1 / ((float)bPm / 60);
        deltaSec = beatSec;
        deltaSecOff = beatSec / beatDivider;
    }

    // Update is called once per frame
    void Update()
    {
        beatSec = 1 / ((float)bPm / 60);
        deltaSec += Time.deltaTime;
        deltaSecOff += Time.deltaTime;
        
        //print(beatSec + " | " + deltaSec);
        
        if(deltaSecOff >= beatSec)
        {
            deltaSecOff = 0;
            current.OffBeat();
        }


        if (deltaSec >= beatSec)
        {
            current.Beat();
            deltaSec = 0;
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
                    takt = (takt < 3) ? takt+1 : 0;
                }
                
            }
        }
    }

    private void EnemyKilled(GameObject obj)
    {
        
        float max = (float)deltaSec - (float)beatSec / 2;
        max = max >= 0 ? max : -max;

        
        float mistake = (float)beatSec / 2 - max;

        float rating = max/((float)beatSec/2);
        // Placeholder output of the rating of the last attack
        print("Rating: " + rating);

        Destroy(obj);
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

    public float getBeatDivided()
    {
        return beatSec / beatDivider;
    }

    public int getBeatDivider()
    {
        return beatDivider;
    }
    
}
