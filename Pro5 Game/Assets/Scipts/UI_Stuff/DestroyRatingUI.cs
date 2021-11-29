using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyRatingUI : MonoBehaviour
{
    public float timeToDestroy;

    private float destroyTime;

    void Start()
    {
        destroyTime = Time.time + timeToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime > Time.time)
        {
            float val = destroyTime - Time.time;
            val /= timeToDestroy;

            Color clr = this.GetComponent<Text>().color;
            clr.a = val;
            this.GetComponent<Text>().color = clr;

            
        }
        else
        {
            Destroy(this);
        }
    }
}
