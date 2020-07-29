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

    private List<Collider2D> singleSyllableWords = new List<Collider2D>();
    private List<Collider2D> multiSyllableWords = new List<Collider2D>();
    private int numberOfSyllables = 0;
    private int syllableTracker = 0;
    private int syllablesHit = 0;

    /* These are numbers to adjust depending on sampling rate of device */
    private float timeBetweenChecks = 0.02f;
        // - Adjust this number to change timing between spike checks (how often to listen for amplitude spike)
    private int amplitudeSpikeCount = 0;
        // - Count spikes during each spike check
    private int amplitudeSpikeCountTarget = 10;
        // - Number of spikes to confirm persistent sound (speech) rather than short spikes (taps)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WordCollider")
        {
            mic.ToggleMicrophone();
            StartCoroutine(tapAnimation.AnimateTapping());
            singleSyllableWords.Add(other);
            amplitudeSpikeCount = 0;
            StartCoroutine(SingleSyllableDetection(other));
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(true);
        }
        else if (other.gameObject.name == "SyllablesCollider")
        {
            numberOfSyllables = other.gameObject.transform.parent.childCount - 1;
            // Minus 1 for SyllableCollider game object -- all other children are Syllables

            mic.ToggleMicrophone();
            amplitudeSpikeCount = 0;
        }
        else if (other.gameObject.name == "SingleSyllableCollider")
        {
            Debug.Log("ON");
            StartCoroutine(tapAnimation.AnimateTapping());
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(true);
        }
    }


    private IEnumerator SingleSyllableDetection(Collider2D other)
    {

        while (singleSyllableWords.Count > 0 && other == singleSyllableWords[0])
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
        CancelInvoke("FilterAudio");
        filtering = false;
        amplitudeSpikeCount = 0;
        Destroy(singleSyllableWords[0].transform.parent.gameObject);
        successSound.Play();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "WordCollider")
        {
            singleSyllableWords.RemoveAt(0);
            mic.ToggleMicrophone();
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(false);
        }
        else if (other.gameObject.name == "SingleSyllableCollider")
        {
            Debug.Log("OFF");
        }
    }

}
