using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public TapAnimationController tapAnimation;
    public MicrophoneComponent mic;
    public List<Collider2D> focusedWords = new List<Collider2D>();
    public AudioSource successSound;

    private int amplitudeTimer = 0;
    private bool filtering = false;
    private int requiredAudioLength = 200;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "OuterCollider")
        {
            mic.ToggleMicrophone();
            StartCoroutine(tapAnimation.AnimateTapping());
            focusedWords.Add(other);
            StartCoroutine(StartDetection(other));
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "OuterCollider")
        {
            focusedWords.RemoveAt(0);
            mic.ToggleMicrophone();
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(false);
        }
    }

    private IEnumerator StartDetection(Collider2D other)
    {

        while (focusedWords.Count > 0 && other == focusedWords[0])
        {
            if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f && !filtering)
            {
                filtering = true;
                InvokeRepeating("FiterAudio", 0.12f, 0.02f);
            }

            yield return null;
        }
    }

    private void FiterAudio()
    {

        if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f)
        {
            amplitudeTimer++;
            if (amplitudeTimer >= requiredAudioLength)
                RegisterHit();
        }

    }

    private void RegisterHit()
    {
        filtering = false;
        amplitudeTimer = 0;
        Destroy(focusedWords[0].transform.parent.gameObject);
    }

}
