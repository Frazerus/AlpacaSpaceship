using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawning : MonoBehaviour
{
    [Tooltip("Indexes are times in beats, values are the enemy Type: 1-4")]
    public int[] enemyFinaltimes;
    public int NrOfEnemiesAtOnce;
    public GameObject[] enemy;

    private int index;
    private int beats;
    private bool doneFlag = false;

    private EnemySpawning spawnEnemy;


    // Start is called before the first frame update
    void Start()
    {
        BeatMachine.current.onKilled += createEnemy;
        BeatMachine.current.onBeat += addBeats;

        spawnEnemy = gameObject.GetComponent<EnemySpawning>();


        for (int i = 0; i < NrOfEnemiesAtOnce; i++)
        {
            createEnemy();
        }
    }



    private void createEnemy()
    {
        if (!doneFlag)
        {
            findNextEnemy();
            spawnEnemy.SpawnEnemy(enemy[enemyFinaltimes[index] - 1], index - beats, enemyFinaltimes[index] - 1);
        }
    }
    private void createEnemy(GameObject obj)
    {
        createEnemy();
    }
    private void addBeats()
    {
        beats++;
    }

    private void findNextEnemy()
    {
        index++;
        while (enemyFinaltimes[index] == 0)
        {
            index++;
            if (index == enemyFinaltimes.Length - 1)
            {
                doneFlag = true;
                break;
            }
        }

    }
}