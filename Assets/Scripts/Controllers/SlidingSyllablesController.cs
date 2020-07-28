﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidingSyllablesController : MonoBehaviour
{
    public float slideSpeed = 2f;
    public Rigidbody2D rb;
    public GameObject syllable;

    private Vector2 movement = new Vector2(-1f, 0f);
    private bool activated = false;
    private float shiftSyllableRight = 485f;

    public void InitializeSlidingSyllables(List<string> syllablesList)
    {
        for (var i = 0; i < syllablesList.Count; i++)
        {
            TextMeshProUGUI word = syllable.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
            word.SetText(syllablesList[i]);

            if (i < syllablesList.Count - 1)
            {
                syllable = Instantiate(syllable, GetComponent<RectTransform>());
                syllable.transform.localPosition = Vector2.zero;
                syllable.transform.localPosition = new Vector2(
                    syllable.transform.localPosition.x + (shiftSyllableRight * (i + 1)),
                    syllable.transform.localPosition.y
                );
            }
        }
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
