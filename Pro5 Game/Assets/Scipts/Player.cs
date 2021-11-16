using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    private GameObject player;
    private Material material;
    void Start()
    {
        player = GameObject.Find("Player");
        material = player.GetComponent<MeshRenderer>().material;
    }


    void Update()
    {
        
    }

    public void changeCol(int col)
    {
        switch (col)
        {
            case 0:
                material.SetColor("_Color", Color.blue);
                break;
            case 1:
                material.SetColor("_Color", Color.green);
                break;
            case 2:
                material.SetColor("_Color", Color.yellow);
                break;
            case 3:
                material.SetColor("_Color", Color.magenta);
                break;
        }
    }

    public void attack(int att)
    {
        changeCol(att);
    }

}
