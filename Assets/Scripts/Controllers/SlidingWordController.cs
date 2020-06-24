using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingWordController : MonoBehaviour
{
    public float slideSpeed = 5f;
    public Rigidbody2D rb;

    private Vector2 movement = new Vector2(-1f, 0f);
    private bool activated = false;

    public void BuildWord(string word)
    {
        Debug.Log(word);
        activated = true;
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            rb.MovePosition(rb.position + movement * slideSpeed * Time.deltaTime);
        }
    }
}
