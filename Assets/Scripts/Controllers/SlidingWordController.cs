﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidingWordController : MonoBehaviour
{
    public float slideSpeed = 2f;
    public Rigidbody2D rb;
    public TextMeshProUGUI textBox;

    private Vector2 movement = new Vector2(-1f, 0f);
    private bool activated = false;


    public void InitializeSlidingWord(string word)
    {
        textBox.text = word;
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
