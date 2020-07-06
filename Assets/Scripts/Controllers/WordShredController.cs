using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordShredController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log(other);
    //}
}
