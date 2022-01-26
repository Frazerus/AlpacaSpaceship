using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public float PerfectKillZone;

    private Animator Anim;

    int AttackHsh = Animator.StringToHash("Attack");
    

    private void Start()
    {
        Anim = this.GetComponent<Animator>();
        
    }

    public void attack(int att)
    {
        print("Attacking: " + att);
        BeatMachine.current.Attack(att);
        Anim.Play("Attack",0,0.4f);

    }

}
