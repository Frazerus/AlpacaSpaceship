using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMachine : MonoBehaviour
{
    public static BeatMachine current;

    [SerializeField] private int bPm = 128;
    [SerializeField] private bool doTick;
    [SerializeField] private int beatDivider = 2; 

    private AudioSource tick;
    private float deltaSec;
    private float deltaSecOffBeat;
    private float beatSec;
    private int takt;
    
    private void Awake()
    {
        current = this;
        tick = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        beatSec = 1 / ((float) bPm/60);
        deltaSec += Time.deltaTime;
        deltaSecOffBeat += Time.deltaTime;
        
        //print(beatSec + " | " + deltaSec);
        if(deltaSecOffBeat%beatSec > beatSec / beatDivider)
        {
            current.OffBeat();
            deltaSecOffBeat = -beatSec/beatDivider;
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

    public event Action onOffBeat;

    public void OffBeat()
    {
        if (onOffBeat != null)
        {
            onOffBeat();
        }
    }

    public event Action onBeat;

    public void Beat()
    {
        if (onBeat != null)
        {
            onBeat();
            
            
        }
    }

    public event Action<int> onAttack;

    public void Attack(int type)
    {
        if (onAttack != null)
        {
            onAttack(type);
        }
    }

}
