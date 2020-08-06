using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawnController : MonoBehaviour
{
    public RectTransform rect;
    public GameObject fretBoard;
    public GameObject slidingWordPrefab;
    public GameObject slidingSyllablesPrefab;

    private GameObject wordClone;
    private GameManager gm;


    private void SpawnSingleSyllableWord(string word)
    {
        wordClone = Instantiate(slidingWordPrefab, GetComponent<RectTransform>());
        wordClone.transform.localPosition = Vector2.zero;
        wordClone.transform.localPosition = new Vector2(
            wordClone.transform.localPosition.x + 525f,
            wordClone.transform.localPosition.y
        );
        wordClone.GetComponent<SlidingWordController>().InitializeSlidingWord(word);
    }

    private void SpawnMultiSyllableWord(string word)
    {

        List<string> syllables = SplitWord(word);
        wordClone = Instantiate(slidingSyllablesPrefab, GetComponent<RectTransform>());
        wordClone.transform.localPosition = Vector2.zero;
        wordClone.transform.localPosition = new Vector2(
            wordClone.transform.localPosition.x + 525f,
            wordClone.transform.localPosition.y
        );
        wordClone.GetComponent<SlidingSyllablesController>().InitializeSlidingSyllables(syllables);
    }

    private List<string> SplitWord(string word)
    {
        List<string> stringList = new List<string>();

        string syllable = "";
        for (var i = 0; i < word.Length; i++)
        {
            if (word[i] == '-')
            {
                stringList.Add(syllable);
                syllable = "";
            }    
            syllable += word[i];
        }
        stringList.Add(syllable);
        return stringList;
    }

    private void Update()
    {
        
    }
}
