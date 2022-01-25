using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingUI : MonoBehaviour
{
    public float[] minRating;
    public string[] ratingNames;
    public float timeToVanish;
    public int maxAlpha;

    public bool Flash = true;
    public int FlashUp = 1;
    public int FlashTimes = 2;
    public float FlashDuration = 0.2f;
    

    private Text txt;
    private Color clr;
    private float Decay;
    private float FlashDecay;

    
    void Start()
    {
        BeatMachine.current.onRating += createRating;
        txt = this.GetComponent<Text>();
        clr = txt.color;

        if (Flash)
        {
            clr.a = 0;
        }
        txt.color = clr;

        Decay = 255/maxAlpha / timeToVanish;
        FlashDecay = 255 / maxAlpha / FlashDuration;


    }

    void FixedUpdate()
    {
        if (Flash)
        {
            clr.a = clr.a + FlashDecay * Time.deltaTime * FlashUp;
            if(clr.a < 0)
            {
                FlashUp = 1;
                if(--FlashTimes < 0)
                {
                    Flash = false;
                }
            }
            else if(clr.a > 1)
            {
                FlashUp = -1;
            }

            txt.color = clr;

        }
        else
        {
            clr.a = clr.a - Decay * Time.deltaTime;
            if (clr.a >= 0)
            {
                txt.color = clr;
            }
        }

        

    }

    private void createRating(float rating, GameObject obj)
    {
        int rate = 0;
        while(rate < minRating.Length && rating < minRating[rate])
        {
            rate++;
        }

        txt.text = ratingNames[rate];
        clr.a = maxAlpha/255;
        txt.color = clr;

    }

}
