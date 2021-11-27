using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public float PerfectKillZone;
    private GameObject player;
    private Material material;
    void Start()
    {
        player = GameObject.Find("Player");
        material = player.GetComponent<MeshRenderer>().material;
        
    }


    public void changeCol(int col)
    {
        ColorSet.changeCol(material, col);
    }

    

    public void attack(int att)
    {
        changeCol(att);
        BeatMachine.current.Attack(att);
    }

}
