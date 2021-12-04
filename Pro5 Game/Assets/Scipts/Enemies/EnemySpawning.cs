using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public void SpawnEnemy(GameObject enemy, int baseDist, int type, int saveDist, bool lastEnemy = false)
    {
        GameObject go = Instantiate(enemy);
        go.GetComponent<Enemy>().BaseDist = baseDist;
        go.GetComponent<Enemy>().EnemyType = type;
        go.GetComponent<Enemy>().savedBeats = saveDist;
        go.GetComponent<Enemy>().lastEnemy = lastEnemy;

        int transNr = Random.Range(0, 4);
        go.transform.position = Constants.directions[transNr];

    }
}