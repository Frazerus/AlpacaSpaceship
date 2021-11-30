using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public void SpawnEnemy(GameObject enemy, int BaseDist, int type)
    {
        GameObject go = Instantiate(enemy);
        go.GetComponent<Enemy>().BaseDist = BaseDist;
        go.GetComponent<Enemy>().EnemyType = type;

        int transNr = Random.Range(0, 4);
        go.transform.position = Constants.directions[transNr];

    }


}
