using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParent : MonoBehaviour
{


    // Start is called before the first frame update
    public void AutoAttackByPlayer()
    {
        this.GetComponentInParent<Enemy>().AutoAttackByPlayer();
    }

    public void EnemyKilled()
    {
        this.GetComponentInParent<Enemy>().EnemyKilled();
    }
}
