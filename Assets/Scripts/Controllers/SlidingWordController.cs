using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidingWordController : MonoBehaviour
{
    public float slideSpeed = 2f;
    public Rigidbody2D rb;
    public RectTransform wordBubble;
    public GameObject textObject;

    private Vector2 movement = new Vector2(-1f, 0f);
    private bool activated = false;

    public void BuildWord(string word)
    {
        float newWidth = wordBubble.rect.width + (word.Length * 40);
        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        SetWordBoxDimensions(newWidth);
        text.text = word;
        activated = true;
    }

    private void SetWordBoxDimensions(float width)
    {
        RectTransform container = GetComponent<RectTransform>();
        RectTransform textRect = textObject.GetComponent<RectTransform>();

        container.sizeDelta = new Vector2(width, container.rect.height);
        wordBubble.sizeDelta = new Vector2(width, wordBubble.rect.height);
        textRect.sizeDelta = new Vector2(width, textRect.rect.height);
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            rb.MovePosition(rb.position + movement * slideSpeed * Time.deltaTime);
        }
    }
}
