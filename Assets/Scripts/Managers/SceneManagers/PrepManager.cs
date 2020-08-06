using UnityEngine;
using System.Collections;

public class PrepManager : MonoBehaviour
{
    public GameObject wordSelectionPrefab;
    public GameObject wordListContainer;

    private GameManager gm;
    private string[] wordList;
    private GameObject currentWord;

    void Start()
    {
        PrepWordData();
        GenerateWordList();
    }

    private void PrepWordData()
    {
        gm = GameManager.instance;
        wordList = gm.GetSelectedWordList();
    }

    private void GenerateWordList()
    {
        for (int wordIndex = 0; wordIndex < wordList.Length; wordIndex++)
        {
            currentWord = Instantiate(wordSelectionPrefab, wordListContainer.transform);
            AlignWordSelectionItem(wordIndex);
            WordProgressModel wordProgress = gm.GetWordProgressData(wordList[wordIndex]);
        }
    }

    private void AlignWordSelectionItem(int wordIndex)
    {
        RectTransform itemRect = currentWord.GetComponent<RectTransform>();
        itemRect.localPosition = new Vector2(
            itemRect.localPosition.x,
            (itemRect.rect.height * wordIndex) + itemRect.localPosition.y);
    }
}
