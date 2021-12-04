using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource baseAudio;
    [SerializeField] private AudioSource  [] layers;
    [Tooltip("Music continues when you get the worst rating but dont miss it completely")]
    [SerializeField] private bool worstRatingMusic;
    [SerializeField] private float fadeTimeBeatPercentage;


    private int[] beatsLeft = {0,0,0,0};
    private bool isFirstBeat = true;
    private int nrOfRatingsOffset;

    private float fadeDuration;

    
    private void Start()
    { 
        nrOfRatingsOffset = worstRatingMusic ? 1 : 2;
        fadeDuration = fadeTimeBeatPercentage * (float)BeatMachine.current.beatSec;

        BeatMachine.current.onOffBeat += ControlLayers;
        BeatMachine.current.onRating += OnRatingAndKill;
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

    private void OnRatingAndKill(float rating, GameObject obj)
    {

        if (rating >= StaticData.minRatings[StaticData.nrOfRatings - nrOfRatingsOffset] && obj.TryGetComponent(out Enemy enemy))
        {
            int enemyType = enemy.EnemyType;

            if (beatsLeft[enemyType] <= 0 ) 
            {
                StartLayer(enemyType);

            }
            // + 1 damit es nicht am beat aufhört sondern einen Beat danach
            beatsLeft[enemyType] = enemy.savedBeats + 1;
        }
    }

    private void StopLayer(int i)
    {
        FadeOutLayer(i);
        //ChangeVolume(i,0);
    }
    
    private void StartLayer(int i)
    {
        FadeInLayer(i);
        //ChangeVolume(i, 1);
    }

    private void ChangeVolume(int layer, float volume)
    {
        layers[layer].volume = volume;
    }

    private void StopAll()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            FadeOutLayer(i);
        }
        StartCoroutine(FadeVolume(baseAudio, 0));
    }


    private void FadeOutLayer(int layer)
    {
        StartCoroutine(FadeVolume(layers[layer],0));
    }

    private void FadeInLayer(int layer)
    {
        StartCoroutine(FadeVolume(layers[layer], 1));
    }

    private IEnumerator FadeVolume(AudioSource layer, float volume)
    {
        float time = 0;
        float start = layer.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            layer.volume = Mathf.Lerp(start, volume, time / fadeDuration);
            yield return null;
        }
        yield break;
    }
    
}
