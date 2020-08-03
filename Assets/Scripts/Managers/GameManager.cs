using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public enum WordListCategories {
        SingleSyllables, MutiSyllables, EasyPhrases, MediumPhrases, HardPhrases
    }
    public Dictionary<string, string[]> completeWordList;
    public string[] selectedWordList = null;


    protected override void OnAwake()
    {
        LoadWords();
    }

    private void LoadWords()
    {
        string wordsJsonString = Resources.Load
            <TextAsset>("Words/WordDictionary").text;
        completeWordList = JsonConvert.DeserializeObject
            <Dictionary<string, string[]>>(wordsJsonString);
    }

    public Dictionary<string, string[]> GetCompleteWordList()
    {
        return completeWordList;
    }

    public string[] GetSelectedWordList()
    {
        return selectedWordList;
    }

    public void SetSelectedWordList(WordListCategories category)
    {
        switch (category)
        {
            case WordListCategories.SingleSyllables:
                selectedWordList = completeWordList["Single Syllable"];
                break;
            case WordListCategories.MutiSyllables:
                selectedWordList = completeWordList["Multi Syllable"];
                break;
            case WordListCategories.EasyPhrases:
                selectedWordList = completeWordList["Easy Phrases"];
                break;
            case WordListCategories.MediumPhrases:
                selectedWordList = completeWordList["Medium Phrases"];
                break;
            case WordListCategories.HardPhrases:
                selectedWordList = completeWordList["Hard Phrases"];
                break;
            default:
                selectedWordList = null;
                break;
        }
    }
}
