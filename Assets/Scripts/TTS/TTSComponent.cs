using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTSComponent : MonoBehaviour
{
    public Text inputText;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            TextToSpeech.instance.Initialize();
        }

        submitButton.onClick.AddListener(Speak);
    }

    private void Speak()
    {
        string line = inputText.text;
        Debug.Log("***********" + line + "***********");

        if (Application.platform == RuntimePlatform.Android && line.Length > 0)
        {
            TextToSpeech.instance.Speak(line);
        }
    }
}
