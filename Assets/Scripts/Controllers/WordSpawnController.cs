using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WordSpawnController : MonoBehaviour
{
    public RectTransform rect;
    public GameObject fretBoard;
    public GameObject slidingWordPrefab;
    public GameObject slidingSyllablesPrefab;

    private GameObject wordClone;
    private GameManager gm;

    const float CADENCE_TIMER = 5f; // Delay between each new word spawn;
    const float PHRASE_TIMER = 2f; // Delay between each word in a phrase;
    private float spawnTimer = 0f;
    private float previousSpawnTime = 0f;
    private bool busySpawning = false;

    private void Start()
    {
        gm = GameManager.instance;
    }

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

    private IEnumerator SpawnPhrase(string word)
    {
        string[] individualWords = word.Split(' ');

        foreach (string w in individualWords)
        {
            if (w.IndexOf('-') > -1)
                SpawnMultiSyllableWord(w);
            else
                SpawnSingleSyllableWord(w);

            yield return new WaitForSeconds(PHRASE_TIMER);
        }
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
        spawnTimer += Time.deltaTime;
       
        if (!busySpawning && (spawnTimer - previousSpawnTime) >= CADENCE_TIMER)
        {
            previousSpawnTime = spawnTimer;
            SpawnWord();
        }
    }

    private void SpawnWord()
    {
        busySpawning = true;
        string word = gm.selectedWord;
        string category = gm.selectedWordCategory;

        switch(category)
        {
            case "Single Syllable":
                SpawnSingleSyllableWord(word);
                break;
            case "Multi Syllable":
                SpawnMultiSyllableWord(word);
                break;
            case "Easy Phrases":
            case "Medium Phrases":
            case "Hard Phrases":
                StartCoroutine(SpawnPhrase(word));
                break;
            default:
                break;
        }

        busySpawning = false;
    }
}
