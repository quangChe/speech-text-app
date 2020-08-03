using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{

    protected override void OnAwake()
    {
        LoadWords();
    }

    public Dictionary<string, string[]> wordList;

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadWords()
    {
        string wordsJsonString = Resources.Load<TextAsset>("Words/WordDictionary").text;
        wordList = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(wordsJsonString);
    }
}
