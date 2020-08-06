using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject levelItemPrefab;
    public GameObject levelItemsContainer;

    private GameManager gm;
    private List<WordCategoryModel> levelCategories = new List<WordCategoryModel>();
    private Dictionary<string, int> levelRequirements = new Dictionary<string, int>();
    private int starsRequiredPerLevel = 30;

    private GameObject itemToAdjust;

    // Use this for initialization
    void Start()
    {
        PrepLevelData();
        SetLevelLockStatus();
        GenerateLevelItems();
    }

    private void PrepLevelData()
    {
        gm = GameManager.instance;
        levelCategories = gm.GetWordCategories();
    }

    private void SetLevelLockStatus()
    {
        int starCounter = 0;

        foreach (WordCategoryModel category in levelCategories)
        {
            levelRequirements.Add(category.categoryName, starCounter);
            starCounter = starCounter + starsRequiredPerLevel;
        }
    }

    private void GenerateLevelItems()
    {
        for (int itemIndex = 0; itemIndex < levelCategories.Count; itemIndex++) 
        {
            itemToAdjust = Instantiate(levelItemPrefab, levelItemsContainer.transform);
            PlaceItemOnUI(itemIndex);
            SetItemView(itemIndex);
        }

        SetContainerDimensions();
    }

    private void PlaceItemOnUI(int itemIndex)
    {
        RectTransform itemRect = itemToAdjust.GetComponent<RectTransform>();
        itemRect.localPosition = new Vector2(
            (itemRect.rect.width * itemIndex) + itemRect.localPosition.x,
            itemRect.localPosition.y);
    }

    private void SetItemView(int itemIndex)
    {
        int playerStars = gm.player.totalStars;
        LevelItemController levelItemCtrl = itemToAdjust.GetComponent<LevelItemController>();
        levelItemCtrl.SetTitle(levelCategories[itemIndex].categoryName);


        if (playerStars >= levelRequirements[levelCategories[itemIndex].categoryName])
        {
            levelItemCtrl.UnlockLevel();
            int starsCollected = levelCategories[itemIndex].starsCollected;
            int starsToCollect = gm.fullWordList[levelCategories[itemIndex].categoryName].Length * 3;
            levelItemCtrl.SetStarInfo(starsCollected, starsToCollect);
        }
        else
        {
            levelItemCtrl.SetLockInfo(levelRequirements[levelCategories[itemIndex].categoryName]);
        }
    }

    private void SetContainerDimensions()
    {
        float itemWidth = itemToAdjust.GetComponent<RectTransform>().rect.width;
        RectTransform containerRect = levelItemsContainer.GetComponent<RectTransform>();
        RectTransform containerParentRect = containerRect.parent.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(itemWidth * levelCategories.Count,
            containerRect.rect.height);
        containerRect.localPosition = new Vector2(
            containerRect.rect.width - containerParentRect.rect.width + containerRect.localPosition.x,
            containerRect.localPosition.y);
    }
}
