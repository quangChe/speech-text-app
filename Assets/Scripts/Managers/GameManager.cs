using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public enum WordListCategories {
        SingleSyllables, MutiSyllables, EasyPhrases, MediumPhrases, HardPhrases
    }

    [System.NonSerialized] public Dictionary<string, string[]> fullWordList;

    [System.NonSerialized] public string[] selectedWordList = null;

    [System.NonSerialized] public DatabaseHelper db;

    [System.NonSerialized] public UserModel player;


    protected override void OnAwake()
    {
        InitializeData();
        LoadWords();
    }

    private void InitializeData()
    {
        db = new DatabaseHelper();
        player = db.Player;
    }

    private void LoadWords()
    {
        string wordsJsonString = Resources.Load
            <TextAsset>("Words/WordDictionary").text;
        fullWordList = JsonConvert.DeserializeObject
            <Dictionary<string, string[]>>(wordsJsonString);
    }

    public string[] GetSelectedWordList()
    {
        return selectedWordList;
    }

    public Dictionary<string, string[]> GetCompleteWordList()
    {
        return fullWordList;
    }

    public void SetSelectedWordList(string categoryName)
    {
        Debug.Log(categoryName);
        selectedWordList = fullWordList[categoryName];
    }

    public List<WordCategoryModel> GetWordCategories()
    {
        return db.WordCategories;
    }

}
