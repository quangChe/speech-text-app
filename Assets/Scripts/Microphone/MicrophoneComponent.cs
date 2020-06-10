using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MicrophoneComponent : MonoBehaviour
{
    public AudioSource audio;
    private string microphone = null;

    void Start()
    {
        if (microphone == null) microphone = Microphone.devices[0];
        Debug.Log(microphone + "*****");
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(microphone, true, 10, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(microphone) > 0))
        {
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
