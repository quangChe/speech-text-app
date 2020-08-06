using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayInfoController : MonoBehaviour
{
    
    public TextMeshProUGUI hitsDisplay;
    public TextMeshProUGUI hitsTargetDisplay;
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI timeTargetDisplay;

    private float timer = 0f;
    private int hits;
    private int hitTarget;
    private int time;
    private int timeTarget;

    // Update is called once per frame
    void Update()
    {
        IncrementTimer();
    }

    private void IncrementTimer()
    {
        timer += Time.deltaTime;
        timeDisplay.text = GetHumanReadableTime(timer);
    }

    private string GetHumanReadableTime(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("0");
        string seconds = (time % 60).ToString("00");
        return string.Format("{0}:{1}", minutes, seconds);
    }

    public void IncrementHit()
    {
        hits++;
        hitsDisplay.text = hits.ToString();
    }
}
