using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSize : MonoBehaviour
{
    void Start()
    {
        float dist =  GameObject.Find("Player").GetComponent<Player>().PerfectKillZone * 2;
        transform.localScale = new Vector3(dist, transform.localScale.y,dist);
    }

    
}
