using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRatingUI : MonoBehaviour
{

    public GameObject prefab;

    public float [] minRating;
    public string [] ratingNames;

    private GameObject UI;

    void Start()
    {
        BeatMachine.current.onRating += createRating;
        UI = GameObject.Find("UI");
    }

    private void createRating(float rating, GameObject obj)
    {
        int i = 0;
        while (i < minRating.Length && rating < minRating[i])
        {
            i++;
        }
        
        GameObject go = Instantiate(prefab) as GameObject;

        go.GetComponent<Text>().text = ratingNames[i];
        //print(ratingNames[i]);
        go.transform.SetParent(UI.transform);
        go.GetComponent<RectTransform>().anchoredPosition = prefab.GetComponent<RectTransform>().anchoredPosition;
    }
}
