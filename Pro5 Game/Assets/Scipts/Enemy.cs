using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int BaseDist;

    private Vector3 dir;
    private GameObject player;
    private int moved = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        dir = player.transform.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        transform.position = -dir * (player.GetComponent<Player>().distFromCloseEnemy + speed * (BaseDist+1));
        transform.position.Set(transform.position.x, 0,transform.position.z);



        float angle = Vector3.Angle(transform.position, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        BeatMachine.current.onBeat += Move;
        
    }

    private void Move()
    {
        if(moved++ == BaseDist)
        {
            BeatMachine.current.onBeat -= Move;
            Destroy(gameObject);
        }
        transform.position += dir * speed;

    }


}
