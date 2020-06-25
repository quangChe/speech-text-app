using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawnController : MonoBehaviour
{
    public RectTransform rect;
    public GameObject fretBoard;
    public GameObject slidingWordPrefab;
    private GameObject wordClone;
    private SlidingWordController wordController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SpawnWord(string word)
    {
        wordClone = Instantiate(slidingWordPrefab, GetComponent<RectTransform>());
        wordClone.transform.localPosition = Vector2.zero;
        wordController = wordClone.GetComponent<SlidingWordController>();
        wordController.BuildWord(word);
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SpawnWord("testing123");
        }
    }
}
