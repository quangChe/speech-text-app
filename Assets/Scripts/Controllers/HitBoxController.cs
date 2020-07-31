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
    private int syllablesHit = 0;
    private bool syllableAlreadyHit = false;

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
            amplitudeSpikeCount = 0;
            ListenForSingleSyllable();
        }
        else if (collision.gameObject.name == "SyllablesCollider")
        {
            ListenForMultipleSyllables();
        }
        else if (collision.gameObject.name == "SingleSyllableCollider")
        {
            syllableAlreadyHit = false; // This is used to make sure only 1 syllable is attempted at a time 
            RunAnimations();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "WordCollider" || collision.gameObject.name == "SyllablesCollider")
        {
            StopListening();
        }
    }

    private void RunAnimations()
    {
        StartCoroutine(tapAnimation.AnimateTapping());
        wordInFocus.transform.parent.GetChild(2).transform.gameObject.SetActive(true);
    }

    private void ListenForSingleSyllable()
    {
        filtering = true;
        filterState = FilterStates.SingleSyllable;

        mic.ToggleMicrophone();
        
        StartCoroutine(RunDetection());
        RunAnimations();
    }

    private void ListenForMultipleSyllables()
    {
        filtering = true;
        filterState = FilterStates.MultiSyllable;
        targetSyllables = wordInFocus.gameObject.transform.parent.childCount - 1; // Minus 1 for SyllablesCollider game object -- all other children are Syllables

        mic.ToggleMicrophone();
        StartCoroutine(RunDetection());
    }


    private IEnumerator RunDetection()
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
            {
                if (filterState == FilterStates.SingleSyllable)
                    RegisterHit();
                else if (filterState == FilterStates.MultiSyllable && !syllableAlreadyHit)
                    RegisterSyllable();
            }
        }
    }

    private void RegisterSyllable()
    {
        syllablesHit++;
        syllableAlreadyHit = true;
        amplitudeSpikeCount = 0; // Reset to prep for next syllable
        Debug.Log("HIT!");
    }


    private void RegisterHit()
    {
        CancelInvoke("FilterAudio");
        filtering = false;
        filterBusy = false;
        Destroy(wordInFocus.transform.parent.gameObject);
        successSound.Play();
    }

    private void StopListening()
    {
        Debug.Log("STOPPED!");
        CancelInvoke("FilterAudio");
        filtering = false;
        filterBusy = false;
        mic.ToggleMicrophone();

        if (filterState == FilterStates.SingleSyllable)
            wordInFocus.transform.parent.GetChild(2).transform.gameObject.SetActive(false);
    }
}
