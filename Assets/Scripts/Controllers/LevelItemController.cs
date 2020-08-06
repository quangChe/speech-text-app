using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelItemController : MonoBehaviour
{
    private GameManager gm;
    public GameObject lockedView;
    public GameObject unlockedView;
    public Button buttonComponent;
    public SceneController sceneController;
    public Image progressFillBar;
    public TextMeshProUGUI title;
    public TextMeshProUGUI lockDetailsText;
    public TextMeshProUGUI progressStarsText;
    public TextMeshProUGUI totalStarsText;

    private void Start()
    {
        gm = GameManager.instance;
    }

    public void SetTitle(string categoryName)
    {
        title.SetText(categoryName);
        buttonComponent.onClick.AddListener(() => {
            gm.SetSelectedWordList(categoryName);
            sceneController.GoToPrepScene();
        });
    }

    public void SetLockInfo(int starsRequired)
    {
        lockDetailsText.SetText($"{starsRequired} Stars to unlock");
    }

    public void SetStarInfo(int accumulated, int totalAvailable)
    {
        progressStarsText.SetText(accumulated.ToString());
        totalStarsText.SetText($"/ {totalAvailable.ToString()}");
        float progressPercent = (accumulated / totalAvailable);
        progressFillBar.fillAmount = progressPercent;
    }

    public void UnlockLevel()
    {
        lockedView.SetActive(false);
        unlockedView.SetActive(true);
    }

}
