using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public TapAnimationController tapAnimation;
    public MicrophoneComponent mic;
    public List<Collider2D> focusedWords = new List<Collider2D>();
    public AudioSource successSound;

    private bool filtering = false;

    /* These are numbers to adjust depending on sampling rate of device */
    private float timeBetweenChecks = 0.02f;
        // - Adjust this number to specify how often to listen for amplitude spike
    private int amplitudeSpikeCount = 0;
        // - Count of spikes each check
    private int amplitudeSpikeCountTarget = 10;
        // - Number of spikes to confirm persistent sound (speech) rather than short spikes (taps)

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
                InvokeRepeating("FiterAudio", 0f, timeBetweenChecks);
            }

            yield return null;
        }
    }

    private void FiterAudio()
    {

        if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f)
        {
            amplitudeSpikeCount++;
            if (amplitudeSpikeCount >= amplitudeSpikeCountTarget)
                RegisterHit();
        }

    }

    private void RegisterHit()
    {
        Debug.Log(amplitudeSpikeCount);
        //CancelInvoke("FilterAudio");
        //filtering = false;
        //amplitudeSpikeCount = 0;
        //Destroy(focusedWords[0].transform.parent.gameObject);
    }

}
