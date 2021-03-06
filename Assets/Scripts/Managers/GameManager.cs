﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public Dictionary<string, string[]> fullWordList;
    [System.NonSerialized] public string[] selectedWordList = null;
    [System.NonSerialized] public DatabaseHelper db;
    [System.NonSerialized] public UserModel player;
    [System.NonSerialized] public string selectedWord;
    [System.NonSerialized] public string selectedWordCategory;

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
        selectedWordList = fullWordList[categoryName];
    }

    public WordProgressModel GetWordProgressData(string word)
    {
        return db.WordProgress.Find((wp) => wp.word == word);
    }

    public void SetSelectedWord(WordProgressModel word)
    {
        selectedWord = word.word;
        WordCategoryModel categoryFound = db.WordCategories.Find(
            (cat) => cat.id == word.categoryId);
        Debug.Log(categoryFound);
        selectedWordCategory = categoryFound.categoryName;
    }

    public List<WordCategoryModel> GetWordCategories()
    {
        return db.WordCategories;
    }

}
