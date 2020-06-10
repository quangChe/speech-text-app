﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneComponent : MonoBehaviour
{
    private string microphone = null;

    public static float MicLoudness;
    public static float MicLoudnessinDecibels;

    public Text decibalText;

    private AudioClip audioClip;
    private AudioClip recordedClip;
    public bool isRecording;
    int audioSampleWindow = 128;


    void OnEnable()
    {
        StartMic();
    }

    // Update is called once per frame
    void Update()
    {
        MicLoudness = MicrophoneLevelMax();
        decibalText.text = MicLoudness.ToString() + " db";
    }

    public void StartMic()
    {
        if (microphone == null) microphone = Microphone.devices[0];
        audioClip = Microphone.Start(microphone, true, 999, 44100);
        isRecording = true;
    }


    public void StopMic()
    {
        Microphone.End(microphone);
        isRecording = false;
    }


    // Grabs mic data and stores as audio clip
    private float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[audioSampleWindow];
        int micPosition = Microphone.GetPosition(null) - (audioSampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        audioClip.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < audioSampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        float decibels = (20 * Mathf.Log10(Mathf.Abs(levelMax)));

        return decibels;
    }

    // Disable any background running recording;
    void OnDisable()
    {
        StopMic();
    }

    void OnDestroy()
    {
        StopMic();
    }

    // Cut off mic if user closes app
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (isRecording)
            {
                StartMic();
                isRecording = true;
            }
        }
        if (!focus)
        {
            StopMic();
            isRecording = false;

        }
    }

}