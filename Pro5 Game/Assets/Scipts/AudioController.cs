using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource baseAudio;
    [SerializeField] private AudioSource layer1;
    [SerializeField] private AudioSource layer2;
    [SerializeField] private AudioSource layer3;
    [SerializeField] private AudioSource layer4;

    private int[] beatsLeft = {0,0,0,0};
    private bool isFirstBeat = true;
    
    private void Start()
    {
        BeatMachine.current.onBeat += ControlleLayers;
        BeatMachine.current.onKilled += OnKill;
    }

    private void StartAll()
    {
        baseAudio.Play();
        layer1.Play();
        layer2.Play();
        layer3.Play();
        layer4.Play();
    }

    private void ControlleLayers()
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
            int enemyTyp = enemy.GetEnemyTyp();
            
            if (beatsLeft[enemyTyp] <= 0)
            {
                StartLayer(enemyTyp);
            }
            // + 1 damit es nicht am beat aufhört sondern einen Beat danach
            beatsLeft[enemyTyp] = enemy.GetSavedBeats() + 1;
        }
    }

    private void StopLayer(int i)
    {
        switch (i)
        {
            case 0:
                layer1.volume = 0;
                break;
            case 1:
                layer2.volume = 0;
                break;
            case 2:
                layer3.volume = 0;
                break;
            case 3:
                layer4.volume = 0;
                break;
        }
    }
    
    private void StartLayer(int i)
    {
        switch (i)
        {
            case 0:
                layer1.volume = 1;
                break;
            case 1:
                layer2.volume = 1;
                break;
            case 2:
                layer3.volume = 1;
                break;
            case 3:
                layer4.volume = 1;
                break;
        }
    }
    
}
