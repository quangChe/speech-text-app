using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    For navigation between scenes
*/

public class SceneController : MonoBehaviour
{

    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToPrepScene()
    {
        SceneManager.LoadScene("PrepScene");
    }
}