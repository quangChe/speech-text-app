using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawnController : MonoBehaviour
{
    public GameObject fretBoard;
    public GameObject slidingWord;

    // Start is called before the first frame update
    void Start()
    {
        GameObject wordClone = Instantiate(slidingWord, fretBoard.transform);
        Debug.Log(wordClone.GetComponent<Rigidbody2D>().position);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
