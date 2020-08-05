using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    private List<WordCategoryModel> wordCategories = new List<WordCategoryModel>();
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.instance;
        GetLevelData();
        BuildLevels();
        CheckPlayerProgress();
    }

    private void GetLevelData()
    {
        gm = GameManager.instance;
        wordCategories = gm.GetWordCategories();
    }

    private void BuildLevels()
    {
        foreach (WordCategoryModel category in wordCategories)
        {

        }
    }

    private void CheckPlayerProgress()
    {

    }
}
