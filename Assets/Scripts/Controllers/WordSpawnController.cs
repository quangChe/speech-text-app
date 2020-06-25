using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawnController : MonoBehaviour
{
    public GameObject fretBoard;
    public GameObject slidingWordPrefab;
    private GameObject wordClone;
    private SlidingWordController wordController;

    // Start is called before the first frame update
    void Start()
    {
        wordClone = Instantiate(slidingWordPrefab, fretBoard.transform);
        wordController = wordClone.GetComponent<SlidingWordController>();
        wordController.BuildWord("testing123");
    }
}
