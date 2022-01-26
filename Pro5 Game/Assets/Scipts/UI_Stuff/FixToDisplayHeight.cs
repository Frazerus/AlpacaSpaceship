using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixToDisplayHeight : MonoBehaviour
{
    public float height;
    public float distFromBot;

    private RectTransform rect;

    void Awake()
    {
        rect = this.GetComponent<RectTransform>();

        Vector3 curr = rect.position;

        print(curr);

        curr.y = Screen.height / 2;
        print(curr);


        

        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, distFromBot, height);
    }

}
