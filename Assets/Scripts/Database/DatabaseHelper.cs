using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Database;
using Newtonsoft.Json;

public class DatabaseHelper
{
    private UserTable users;

    private WordCategoryTable wordCategories;

    public UserModel Player { private set; get; }

    public List<WordCategoryModel> WordCategories { private set; get; } = new List<WordCategoryModel>();

    public DatabaseHelper()
    {
        users = new UserTable();
        wordCategories = new WordCategoryTable();
        
        GetOrCreateUser();
        GetOrCreateWordCategories();
    }

    private void GetOrCreateUser()
    {
        Player = users.GetById(1);
        if (Player == null) CreateDefaultPlayer();
        else UpdateLoginTime();
    }

    private void GetOrCreateWordCategories()
    {
        WordCategories = wordCategories.GetByUserId(Player.id);
        if (WordCategories.Count == 0) CreateDefaultWordCategories();
    }

    private void CreateDefaultPlayer()
    {
        users.Create(new UserModel { name = "Main User" });
        Player = users.GetById(1);
    }

    private void CreateDefaultWordCategories()
    {
        string wordsJsonString = Resources.Load<TextAsset>("Words/WordDictionary").text;
        Dictionary<string, string[]> wordList = new Dictionary<string, string[]>();
        wordList = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(wordsJsonString);

        foreach (string key in wordList.Keys)
        {
            wordCategories.Create(new WordCategoryModel {
                userId = Player.id, categoryName = key, starsCollected = 0});
        }
    }


    public void UpdateUser(UserModel user)
    {
        users.UpdateUser(user);
    }

    private void UpdateLoginTime()
    {
        users.UpdateLoginDate(Player.id);
        Player = users.GetById(Player.id);
    }

    public void CloseConnections()
    {
        users.Close();
    }

}
