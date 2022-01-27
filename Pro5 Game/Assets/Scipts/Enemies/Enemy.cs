using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int BaseDist;
    public int EnemyType;
    //public GameObject child;
    public float distOffset = 0.5f;

    public bool lastEnemy = false;

    public int savedBeats { get; set; }


    private float beatTime;
    private Vector3 dir;
    private GameObject player;
    private Player playerScript;
    private int moved = 0;
    private Material material;
    private bool moving = false;
    private double beatOffset;
    private Animator Anim;



    private float[] enemySize =
    {
        0.5f,
        0.5f,
        0.5f,
        0.5f,
    };

    void Start()
    {
        //material = this.GetComponent<MeshRenderer>().material;
        //Constants.changeCol(material, EnemyType);

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        dir = player.transform.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        Anim = GetComponentInChildren<Animator>();


        //print(dir);
        if (dir.z < 0 && dir.x < 0)
        {
            transform.Rotate(new Vector3(0, 180, 0));

        }


        BeatMachine.current.onBeat += Move;
        BeatMachine.current.onAttack += Attacked;

        beatOffset = BeatMachine.current.beatSec * 0.5f * speed;

    }

    private void Update()
    {
        if (moving)
        {
            //Lineare Bewegung
            float inBeatDiff = (Time.time - beatTime) / (float)BeatMachine.current.beatSec;
            //jumping
            //inBeatDiff = 0;
            transform.position = -dir * (playerScript.PerfectKillZone + distOffset + speed * (BaseDist - moved - inBeatDiff));
        }
    }



    private void Move()
    {

        if (!moving)
        {
            moving = true;
        }
        beatTime = Time.time;

        if (moved   >= BaseDist)
        {
            moving = false;
            Anim.Play("Attack");
            BeatMachine.current.Rating(0, gameObject);
            BeatMachine.current.onBeat -= Move;
            //EnemyKilled();

        }
        moved++;

        //transform.position += dir * speed;

    }


    private void Attacked(int type)
    {
        if (type == EnemyType && BaseDist - moved < 1 + beatOffset)
        {
            
            //EnemyKilled();
            Anim.Play("Death");
            if (moving)
            {
                BeatMachine.current.CreateRatingAndSend(gameObject);
            }
            moving = false;
            BeatMachine.current.onBeat -= Move;
            BeatMachine.current.onAttack -= Attacked;
        }
    }

    public int GetEnemyType()
    {
        return EnemyType;
    }

    public int GetSavedBeats()
    {
        return savedBeats;
    }

    public void EnemyKilled()
    {


        if (lastEnemy)
        {
            BeatMachine.current.EndPlaying();
        }


        BeatMachine.current.Killed(gameObject);

        Destroy(gameObject);
    }

    public void AutoAttackByPlayer()
    {
            playerScript.attack(EnemyType);
    }



}
