using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public TapAnimationController tapAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "OuterCollider")
        {
            StartCoroutine(tapAnimation.AnimateTapping());
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "OuterCollider")
        {
            GameObject lightBubble = other.transform.parent.GetChild(2).transform.gameObject;
            lightBubble.SetActive(false);
        }
    }
}
