using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSet 
{
   
    public static Color _1 = Color.red;
    public static Color _2 = Color.green;
    public static Color _3 = Color.blue;
    public static Color _4 = Color.yellow;

    public static void changeCol(Material material, int col)
    {
        switch (col)
        {
            case 0:
                material.SetColor("_Color", ColorSet._1);
                break;
            case 1:
                material.SetColor("_Color", ColorSet._2);
                break;
            case 2:
                material.SetColor("_Color", ColorSet._3);
                break;
            case 3:
                material.SetColor("_Color", ColorSet._4);
                break;
        }
    }
}
