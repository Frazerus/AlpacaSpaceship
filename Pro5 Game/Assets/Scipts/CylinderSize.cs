using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSize : MonoBehaviour
{
    void Start()
    {
        float dist = 1.0f + GameObject.Find("Player").GetComponent<Player>().distFromCloseEnemy + (1.0f / BeatMachine.current.getBeatDivider());
        transform.localScale = new Vector3(dist, transform.localScale.y,dist);
    }

    
}
