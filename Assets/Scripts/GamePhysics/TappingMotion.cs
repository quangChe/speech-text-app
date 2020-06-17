using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappingMotion : MonoBehaviour
{
    public GameObject[] path;


    private void Start()
    {
        StartCoroutine(AnimateTap());
    }

    public IEnumerator AnimateTap()
    {
        int i = 0;

        while (i < path.Length)
        {
            transform.position = Vector3.MoveTowards(
                          transform.position,
                          path[i].transform.position,
                          Time.deltaTime * 10
                       );

            transform.Rotate(0, 0, -0.5f);

            if (transform.position == path[i].transform.position)
                i++;

            yield return null;
        }

        i = path.Length - 1;

        while (i >= 0)
        {
            transform.position = Vector3.MoveTowards(
                          transform.position,
                          path[i].transform.position,
                          Time.deltaTime * 10
                       );

            transform.Rotate(0, 0, 0.5f);

            if (transform.position == path[i].transform.position)
                i--;

            yield return null;
        }
    }
}
