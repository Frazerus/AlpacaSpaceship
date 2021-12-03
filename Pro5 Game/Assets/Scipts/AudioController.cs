using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource baseAudio;
    [SerializeField] private AudioSource  [] layers;


    private int[] beatsLeft = {0,0,0,0};
    private bool isFirstBeat = true;
    
    private void Start()
    {
        BeatMachine.current.onOffBeat += ControlLayers;
        BeatMachine.current.onKilled += OnKill;
        BeatMachine.current.onEnd += StopAll;
    }

    private void StartAll()
    {
        baseAudio.Play();
        layers[0].Play();
        layers[1].Play();
        layers[2].Play();
        layers[3].Play();
    }

    private void ControlLayers()
    {
        if (isFirstBeat)
        {
            StartAll();
            isFirstBeat = false;
        }

        for (int i = 0; i < beatsLeft.Length; i++)
        {
            beatsLeft[i]--;
            if (beatsLeft[i] <= 0)
            {
                StopLayer(i);
            }
        }
    }

    private void OnKill(GameObject obj)
    {

        if (obj.TryGetComponent(out Enemy enemy))
        {
            int enemyType = enemy.GetEnemyType();
            
            if (beatsLeft[enemyType] <= 0)
            {
                StartLayer(enemyType);
            }
            // + 1 damit es nicht am beat aufhört sondern einen Beat danach
            beatsLeft[enemyType] = enemy.GetSavedBeats() + 1;
        }
    }

    private void StopLayer(int i)
    {
        ChangeVolume(i,0);
    }
    
    private void StartLayer(int i)
    {
        ChangeVolume(i, 1);
    }

    private void ChangeVolume(int layer, float volume)
    {
        layers[layer].volume = volume;
    }

    private void StopAll()
    {
        StartCoroutine("StopAllIE");
    }

    private IEnumerator StopAllIE()
    {
        yield return new WaitForSeconds(10);
        for(int i = 0; i < layers.Length; i ++)
        {
            layers[i].Stop();
        }
        baseAudio.Stop();
    }
    
}
