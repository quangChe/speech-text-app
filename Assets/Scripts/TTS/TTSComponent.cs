using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTSComponent : MonoBehaviour
{
    public Text inputText;
    public Button submitButton;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            TextToSpeech.instance.Initialize();
            submitButton.onClick.AddListener(Speak);
        }
    }

    private void Speak()
    {
        string line = inputText.text;

        if (line.Length > 0)
        {
            TextToSpeech.instance.Speak(line);
        }
    }
}
