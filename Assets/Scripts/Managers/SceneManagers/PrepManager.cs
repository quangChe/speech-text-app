using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PrepManager : MonoBehaviour
{
    public GameObject wordSelectionPrefab;
    public GameObject wordListContainer;
    public TextMeshProUGUI videoLabel;

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
            InitializeWordButton(wordIndex);
            
        }
    }

    private void AlignWordSelectionItem(int wordIndex)
    {
        RectTransform itemRect = currentWord.GetComponent<RectTransform>();
        itemRect.localPosition = new Vector2(
            itemRect.localPosition.x,
            itemRect.localPosition.y - (itemRect.rect.height * wordIndex));
    }

    private void InitializeWordButton(int wordIndex)
    {
        WordProgressModel wordProgress = gm.GetWordProgressData(wordList[wordIndex]);
        currentWord.GetComponent<WordSelectionController>().SetWordProgress(wordProgress);
        currentWord.GetComponent<Button>().onClick.AddListener(() =>
        {
            string selectedWord = wordList[wordIndex];
            gm.SetSelectedWord(selectedWord);
            SetVideoDetails(selectedWord);
        });
    }

    private void SetVideoDetails(string word)
    {
        videoLabel.SetText(word);
    }
}
