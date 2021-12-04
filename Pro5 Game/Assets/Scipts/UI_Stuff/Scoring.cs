using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private double total;
    private int count;

    private Text text;
    void Start()
    {
        BeatMachine.current.onRating += addScore;
        BeatMachine.current.onEnd += saveScore;

        text = gameObject.GetComponent<Text>();
    }


    private void addScore(float rating, GameObject obj)
    {
        total += rating;
        count++;
    }

    private void saveScore()
    {
        StaticData.score = total / count;

    }

}
