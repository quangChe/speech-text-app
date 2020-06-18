using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingWordController : MonoBehaviour
{
    public float slideSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement = new Vector2(-1f, 0f);

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * slideSpeed * Time.deltaTime);
    }
}
