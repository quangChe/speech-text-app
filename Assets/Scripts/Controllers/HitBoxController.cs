using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public TapAnimationController tapAnimation;
    public MicrophoneComponent mic;
    public List<Collider2D> focusedWords = new List<Collider2D>();
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void IncrementTimer()
    {
        timer += 0.01f;
    }


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
        InvokeRepeating("IncrementTimer", 0f, 0.01f);

        while (focusedWords.Count > 0 && other == focusedWords[0])
        {
            if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f)
            {
                yield return new WaitForSeconds(0.1f);
                if (mic.MicrophoneLevelMax() < 0f && mic.MicrophoneLevelMax() > -50f)
                    //Debug.Log(mic.MicrophoneLevelMax() + " - " + timer + "sec");
                    Destroy(other.transform.parent.gameObject);
            }

            yield return null;
        }
    }
}
