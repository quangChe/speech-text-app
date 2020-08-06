using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordSelectionController : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public GameObject[] stars;

    private WordProgressModel word;

    public void SetWordProgress(WordProgressModel wordProgress)
    {
        word = wordProgress;
        wordText.SetText(wordProgress.word);
        AssignStarsBasedOnHits();
    }

    private void AssignStarsBasedOnHits()
    {
        switch(word.timesHit)
        {
            case 10: // 1/3 stars
                GrantCompleteStar(stars[0]);
                break;
            case 20: // 2/3 stars
                GrantCompleteStar(stars[1]);
                break;
            case 30: // 3/3 stars
                GrantCompleteStar(stars[2]);
                break;
            default:
                break;
        }
    }

    private void GrantCompleteStar(GameObject star)
    {
        star.transform.GetChild(0).gameObject.SetActive(false);
        star.transform.GetChild(1).gameObject.SetActive(false);
    }
}
