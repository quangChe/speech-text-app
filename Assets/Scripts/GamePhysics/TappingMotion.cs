using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappingMotion : MonoBehaviour
{
    public GameObject[] path;
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
        yield return StartCoroutine(HandDownMotion());
        yield return StartCoroutine(HandUpMotion());
    }

    private IEnumerator HandDownMotion()
    {
        int i = 0;

        while (i < path.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                path[i].transform.position, Time.deltaTime * 15);

            if (transform.position == path[i].transform.position) i++;

            yield return null;
        }


        AddExtraEffects();
    }

    private IEnumerator HandUpMotion()
    {
        int i = path.Length - 1;

        while (i >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                path[i].transform.position, Time.deltaTime * 15);

            if (transform.position == path[i].transform.position) i--;

            yield return null;
        }

    }

    private void AddExtraEffects()
    {
        drumSound.Play();
    }
}
