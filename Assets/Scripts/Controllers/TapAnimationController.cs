using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapAnimationController : MonoBehaviour
{
    public GameObject[] path;
    public GameObject hand;
    public GameObject textPrompt;
    public AudioSource drumSound;

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            StartCoroutine(AnimateTapping());
        }
    }

    public IEnumerator AnimateTapping()
    {
        yield return StartCoroutine(ShowTextPrompt());
        yield return StartCoroutine(HandDownMotion());
        StartCoroutine(HideTextPrompt());
        StartCoroutine(HandUpMotion());
    }

    private IEnumerator HandDownMotion()
    {
        int i = 0;

        while (i < path.Length)
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position,
                path[i].transform.position, Time.deltaTime * 18);

            if (hand.transform.position == path[i].transform.position) i++;

            yield return null;
        }

        drumSound.Play();
    }

    private IEnumerator HandUpMotion()
    {
        int i = path.Length - 1;

        while (i >= 0)
        {
            hand.transform.position = Vector3.MoveTowards(hand.transform.position,
                path[i].transform.position, Time.deltaTime * 18);

            if (hand.transform.position == path[i].transform.position) i--;

            yield return null;
        }

    }


    private IEnumerator ShowTextPrompt()
    {
        Vector3 scale = textPrompt.transform.localScale;
        while (scale.x <= 1.2f)
        {
            textPrompt.transform.localScale = new Vector3(scale.x + 0.02f, scale.y + 0.02f);
            scale = textPrompt.transform.localScale;
            Debug.Log(scale);
            yield return null;
        }
    }

    private IEnumerator HideTextPrompt()
    {
        Vector3 scale = textPrompt.transform.localScale;
        while (scale.x >= 0f)
        {
            textPrompt.transform.localScale = new Vector3(scale.x - 0.02f, scale.y - 0.02f);
            scale = textPrompt.transform.localScale;
            yield return null;
        }
    }
}
