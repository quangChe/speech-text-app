using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using Database;
using Newtonsoft.Json;

public class DatabaseHelper
{
    private Dictionary<string, string[]> WordDict = null;

    private UserTable users;
    private WordCategoryTable wordCategories;
    private WordProgressTable wordProgress;

    public UserModel Player { private set; get; }
    public List<WordCategoryModel> WordCategories { private set; get; } = new List<WordCategoryModel>();
    public List<WordProgressModel> WordProgress { private set; get; } = new List<WordProgressModel>();

    
    public DatabaseHelper()
    {
        users = new UserTable();
        wordCategories = new WordCategoryTable();
        wordProgress = new WordProgressTable(); 

        GetOrCreateUser();
        GetOrCreateWordCategoriesAndProgress();
    }

    private void GetWordDictionary()
    {
        if (WordDict == null)
        {
            string wordsJsonString = Resources.Load<TextAsset>("Words/WordDictionary").text;
            WordDict = new Dictionary<string, string[]>(
                JsonConvert.DeserializeObject<Dictionary<string, string[]>>(wordsJsonString));
        }
    }


    private void GetOrCreateUser()
    {
        Player = users.GetById(1);
        if (Player == null) CreateDefaultPlayer();
        else UpdateLoginTime();
    }

    private void GetOrCreateWordCategoriesAndProgress()
    {
        WordCategories = wordCategories.GetByUserId(Player.id);
        if (WordCategories.Count == 0) CreateDefaultWordCategories();

        WordProgress = wordProgress.GetByUserId(Player.id);
        if (WordProgress.Count == 0) CreateDefaultWordProgress();
    }


    private void CreateDefaultPlayer()
    {
        users.Create(new UserModel { name = "Main User" });
        Player = users.GetById(1);
    }

    private void CreateDefaultWordCategories()
    {

        foreach (string key in WordDict.Keys)
        {
            wordCategories.Create(new WordCategoryModel {
                userId = Player.id, categoryName = key, starsCollected = 0
            });
        }
    }

    private void CreateDefaultWordProgress()
    {
        GetWordDictionary();

        foreach (string key in WordDict.Keys)
        {
            WordCategoryModel category = WordCategories.Find((cat) => cat.categoryName == key);
            Debug.Log(category.id);
            foreach (string word in WordDict[key])
            {
                Debug.Log(wordProgress);
                wordProgress.Create(new WordProgressModel {
                    word = word, timesHit = 0, timesAttempted = 0,
                    userId = Player.id, categoryId = category.id
                });
            }
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
