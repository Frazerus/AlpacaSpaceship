using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public float PerfectKillZone;

    public AudioClip hitSound;
    public AudioClip missSound;

    public AudioSource audio;

    private Animator Anim;

    int AttackHsh = Animator.StringToHash("Attack");
    

    private void Start()
    {
        Anim = this.GetComponent<Animator>();

        BeatMachine.current.onHit += hit;
        
    }

    public void attack(int att)
    {
        //print("Attacking: " + att);
        BeatMachine.current.Attack(att);
        Anim.Play("Attack");

    }

    private void hit()
    {
        //print("GET HIT NOW");
        Anim.Play("getHit");
    }

    public void PlayHit()
    {
        audio.clip = hitSound;
        audio.Play();
    }

    public void PlayMiss()
    {
        audio.clip = missSound;
        audio.Play();
    }

}
