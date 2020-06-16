using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMotion : MonoBehaviour
{
    public float slideSpeed = 5.0F;
    public Rigidbody2D rb;

    private Vector2 movement = new Vector2(-1f, 0f);

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * slideSpeed * Time.deltaTime);
    }
}
