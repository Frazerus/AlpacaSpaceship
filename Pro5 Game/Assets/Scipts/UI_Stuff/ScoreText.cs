using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Text>().text = (Math.Round(StaticData.score * 100,2)).ToString();
    }
}
