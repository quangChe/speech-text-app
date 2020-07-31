using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayInfoManager : MonoBehaviour
{
    
    public TextMeshProUGUI hitsUI;
    public TextMeshProUGUI hitTargetUI;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI timeTargetUI;

    private float timer;
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
        timeUI.text = GetHumanReadableTime(timer);
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
        hitsUI.text = hits.ToString();
    }
}
