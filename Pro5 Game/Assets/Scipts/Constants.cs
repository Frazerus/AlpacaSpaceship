using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    public static Color color_1 = Color.red;
    public static Color color_2 = Color.green;
    public static Color color_3 = Color.blue;
    public static Color color_4 = Color.yellow;


    public static int directionMult = 50;
    public static Vector3[] directions =
    {
        Vector3.forward*directionMult,
        Vector3.right*directionMult,
        Vector3.back*directionMult,
        Vector3.left*directionMult
    };


    public static void changeCol(Material material, int col)
    {
        switch (col)
        {
            case 0:
                material.SetColor("_Color", color_1);
                break;
            case 1:
                material.SetColor("_Color", color_2);
                break;
            case 2:
                material.SetColor("_Color", color_3);
                break;
            case 3:
                material.SetColor("_Color", color_4);
                break;
        }
    }
}
