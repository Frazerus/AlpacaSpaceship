using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int BaseDist;
    public int EnemyType;
    [SerializeField] private int savedBeats = 16;
    
    private Vector3 dir;
    private GameObject player;
    private int moved = 0;
    private Material material;
    private bool moving = false;

    void Start()
    {
        material = this.GetComponent<MeshRenderer>().material;
        ColorSet.changeCol(material, EnemyType);


        player = GameObject.Find("Player");
        dir = player.transform.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        transform.position = -dir * (1 + player.GetComponent<Player>().distFromCloseEnemy + speed * (BaseDist + 1/BeatMachine.current.getBeatDivider()));
        transform.position.Set(transform.position.x, 0,transform.position.z);

        float angle = Vector3.Angle(transform.position, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        BeatMachine.current.onOffBeat += Move;
        BeatMachine.current.onAttack += Attacked;
        
    }

    private void Update()   
    {
        if (moving)
        {
            transform.position += dir * speed * (float)(Time.deltaTime/BeatMachine.current.beatSec);
        }
    }



    private void Move()
    {
        
        //moving = true;
        if(moved++ == BaseDist)
        {
            BeatMachine.current.onOffBeat -= Move;
            BeatMachine.current.onAttack -= Attacked;
            print(transform.position.magnitude);

            Destroy(gameObject);
        }
        transform.position += dir * speed;

    }


    private void Attacked(int type)
    {
        if(type == EnemyType && BaseDist - moved <  1)
        {

            BeatMachine.current.onOffBeat -= Move;
            BeatMachine.current.onAttack -= Attacked;

            BeatMachine.current.Killed(gameObject);
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

}
