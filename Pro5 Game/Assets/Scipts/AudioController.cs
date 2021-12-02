using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    /*
     * Maybe mit nem Array an audiosources? dann musst du nicht unten die ganze zeit switch statements machen
     */
    [SerializeField] private AudioSource baseAudio;
    [SerializeField] private AudioSource layer1;
    [SerializeField] private AudioSource layer2;
    [SerializeField] private AudioSource layer3;
    [SerializeField] private AudioSource layer4;

    [SerializeField] private float musicTimeOffset;

    private int[] beatsLeft = {0,0,0,0};
    private bool isFirstBeat = true;
    
    private void Start()
    {
        //DO NOt FUCKING ASK ME WHY IT HAS TO BE OFFBEAT;
        //I WOULD LOVE TO KNOW TOO
        //FUCK THIS SHIT
        BeatMachine.current.onOffBeat += ControlLayers;
        BeatMachine.current.onKilled += OnKill;
    }

    private IEnumerator StartAll()
    {
        yield return new WaitForSeconds(musicTimeOffset);
        baseAudio.Play();
        layer1.Play();
        layer2.Play();
        layer3.Play();
        layer4.Play();
    }

    private void ControlLayers()
    {
        if (isFirstBeat)
        {
            StartCoroutine(StartAll());
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
        changeVolume(i,0);
    }
    
    private void StartLayer(int i)
    {
        changeVolume(i, 1);
    }

    private void changeVolume(int layer, float volume)
    {
        switch (layer)
        {
            case 0:
                layer1.volume = volume;
                break;
            case 1:
                layer2.volume = volume;
                break;
            case 2:
                layer3.volume = volume;
                break;
            case 3:
                layer4.volume = volume;
                break;
        }
    }
    
}
