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
            starCounter += starsRequiredPerLevel;
        }
    }

    private void GenerateLevelItems()
    {
        int playerStars = gm.player.totalStars;
        float itemWidth = 0;

        for (int i = 0; i < levelCategories.Count; i++) 
        {
            GameObject levelItem = Instantiate(levelItemPrefab, levelItemsContainer.transform);
            RectTransform itemRect = levelItem.GetComponent<RectTransform>();
            itemRect.localPosition = new Vector2(
                (itemRect.rect.width * i) + itemRect.localPosition.x,
                itemRect.localPosition.y);

            itemWidth = itemRect.rect.width;
        }

        RectTransform containerRect = levelItemsContainer.GetComponent<RectTransform>();
        RectTransform containerParentRect = containerRect.parent.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(itemWidth * levelCategories.Count,
            containerRect.rect.height);
        containerRect.localPosition = new Vector2(
            containerRect.rect.width - containerParentRect.rect.width + containerRect.localPosition.x,
            containerRect.localPosition.y);
    }
}
