using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawning : MonoBehaviour
{
    [Tooltip("Indexes are times in beats, values are the enemy Type: 1-3")]
    public int[] enemyFinaltimes;
    public int NrOfEnemiesAtOnce;
    public GameObject[] enemy;

    private int index;
    private int beats;
    private bool doneFlag = false;
    private int lastEnemyIndex;

    private EnemySpawning spawnEnemy;


    // Start is called before the first frame update
    void Start()
    {
        BeatMachine.current.onKilled += createEnemy;
        BeatMachine.current.onBeat += addBeats;

        spawnEnemy = gameObject.GetComponent<EnemySpawning>();

        lastEnemyIndex = findLastEnemy();
        //print("lastEnemyIndex: " + lastEnemyIndex);

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
            if (index != lastEnemyIndex)
            {
                int saveDist = findNextEnemyByType(enemyFinaltimes[index]) - index;
                //print("Trying to create Enemy: Arraypos: " + index);
                spawnEnemy.SpawnEnemy(
                            enemy: enemy[enemyFinaltimes[index] - 1],
                            baseDist: index - beats,
                            type: enemyFinaltimes[index] - 1,
                            saveDist: saveDist
                            );
            }
            else
            {
                spawnEnemy.SpawnEnemy(
                enemy: enemy[enemyFinaltimes[index] - 1],
                baseDist: index - beats,
                type: enemyFinaltimes[index] - 1,
                saveDist: enemyFinaltimes.Length - index,
                lastEnemy: true
                );
            }

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
            if (index >= lastEnemyIndex)
            {
                //print("Setting done flag at index: " + index);
                doneFlag = true;
                break;
            }
        }

    }

    private int findNextEnemyByType(int type)
    {
        int i = index + 1;
        while (i < enemyFinaltimes.Length && enemyFinaltimes[i] != type)
        {
            i++;
        }
        return i;
    }

    private int findLastEnemy()
    {
        for (int i = enemyFinaltimes.Length - 1; i > 0; i--)
        {
            if (enemyFinaltimes[i] != 0)
            {
                return i;
            }
        }
        return 0;
    }
}