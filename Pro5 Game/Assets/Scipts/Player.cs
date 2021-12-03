using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public float PerfectKillZone;
    private GameObject player;
  

    public void attack(int att)
    {
        BeatMachine.current.Attack(att);
    }

}
