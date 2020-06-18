using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnimationController : MonoBehaviour
{
    public GameObject[] path;
    public GameObject hand;
    public AudioSource drumSound;
    public GameObject textPrompt;

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            StartCoroutine(AnimateTapping());
        }
    }

    public IEnumerator AnimateTapping()
    {
        yield return StartCoroutine(HandDownMotion());
        StartCoroutine(HandUpMotion());
    }

    private IEnumerator HandDownMotion()
    {
        int i = 0;

        while (i < path.Length)
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position,
                path[i].transform.position, Time.deltaTime * 15);

            if (hand.transform.position == path[i].transform.position) i++;

            yield return null;
        }


        AddExtraEffects();
    }

    private IEnumerator HandUpMotion()
    {
        int i = path.Length - 1;

        while (i >= 0)
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position,
                path[i].transform.position, Time.deltaTime * 15);

            if (hand.transform.position == path[i].transform.position) i--;

            yield return null;
        }

    }

    private void AddExtraEffects()
    {
        drumSound.Play();
    }
}
