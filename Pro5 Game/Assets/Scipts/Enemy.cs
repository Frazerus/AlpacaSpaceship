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

        transform.position = -dir * (player.GetComponent<Player>().distFromCloseEnemy + speed * BaseDist);
        transform.position.Set(transform.position.x, 0,transform.position.z);

        float angle = Vector3.Dot(transform.position, Vector3.forward);
        angle /= Vector3.Magnitude(transform.position);
        print(angle);
        angle = Mathf.Acos(angle*(Mathf.PI/180));

        print(angle);

        transform.rotation = Quaternion.Euler(0, angle, 0);

        InvokeRepeating("move", 0, 1);
        
    }

    private void move()
    {
        if(moved++ >= BaseDist)
        {
            Destroy(gameObject);
        }
        transform.position += dir * speed;

    }


}
