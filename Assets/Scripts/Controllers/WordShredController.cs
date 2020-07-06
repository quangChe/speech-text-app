using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordShredController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log(other);
    }
}
