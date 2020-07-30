using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public TapAnimationController tapAnimation;
    public MicrophoneComponent mic;
    public AudioSource successSound;

    private enum FilterStates { SingleSyllable, MultiSyllable };
    private FilterStates filterState;
    private bool filtering = false;
    private bool filterBusy = false;

    private Collider2D wordInFocus;
    private int targetSyllables = 0;
    private int currentSyllable = 0;
    private int syllablesHit = 0;

    /* These are numbers to adjust depending on sampling rate of device */
    private float timeBetweenChecks = 0.02f;
        // - Adjust this number to change timing between spike checks (how often to listen for amplitude spike)
    private int amplitudeSpikeCount = 0;
        // - Count spikes during each spike check
    private int amplitudeSpikeCountTarget = 10;
        // - Number of spikes to confirm persistent sound (speech) rather than short spikes (taps)
        // - On a MacBook with default mic, a normal tap registers about 5 spikes while a short syllable registers 10 spikes


    void OnTriggerEnter2D(Collider2D collision)
    {
        wordInFocus = collision;

        if (collision.gameObject.name == "WordCollider")
        {
            StartSingleSyllableFilterMode();
        }
        else if (collision.gameObject.name == "SyllablesCollider")
        {
            targetSyllables = collision.gameObject.transform.parent.childCount - 1;
            // Minus 1 for SyllableCollider game object -- all other children are Syllables

            mic.ToggleMicrophone();
            amplitudeSpikeCount = 0;
        }
        else if (collision.gameObject.name == "SingleSyllableCollider")
        {
            currentSyllable++;
            StartCoroutine(tapAnimation.AnimateTapping());
            GameObject lightBubble = wordInFocus.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "WordCollider")
        {
            EndSingleSyllableFilterMode();
        }
        else if (collision.gameObject.name == "SingleSyllableCollider")
        {
            Debug.Log("OFF");
        }
    }


    private void StartSingleSyllableFilterMode()
    {
        filtering = true;
        filterState = FilterStates.SingleSyllable;
        mic.ToggleMicrophone();
        StartCoroutine(tapAnimation.AnimateTapping());
        StartCoroutine(SingleSyllableDetection());
        wordInFocus.transform.parent.GetChild(2).transform.gameObject.SetActive(true);
    }


    private IEnumerator SingleSyllableDetection()
    {
        while (filtering)
        {
            if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f && !filterBusy)
                InvokeRepeating("FiterAudio", 0f, timeBetweenChecks);

            yield return null;
        }
    }

    private void FiterAudio()
    {
        filterBusy = true;

        if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f)
        {
            amplitudeSpikeCount++;
            if (amplitudeSpikeCount >= amplitudeSpikeCountTarget)
                RegisterHit();
        }

    }

    private void RegisterHit()
    {
        CancelInvoke("FilterAudio");
        filtering = false;
        filterBusy = false;
        amplitudeSpikeCount = 0;
        Destroy(wordInFocus.transform.parent.gameObject);
        successSound.Play();
    }

    private void EndSingleSyllableFilterMode()
    {
        CancelInvoke("FilterAudio");
        filtering = false;
        filterBusy = false;
        amplitudeSpikeCount = 0;
        mic.ToggleMicrophone();
        wordInFocus.transform.parent.GetChild(2).transform.gameObject.SetActive(false);
    }
}
