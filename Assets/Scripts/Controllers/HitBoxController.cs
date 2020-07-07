using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
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
        GameObject lightBubble = other.transform.GetChild(2).transform.gameObject;
        lightBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject lightBubble = other.transform.GetChild(2).transform.gameObject;
        lightBubble.SetActive(false);
    }
}
