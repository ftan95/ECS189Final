using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevel()
    {
        switch (this.gameObject.name)
        {
            case "New Game Button":
                SceneManager.LoadScene("Intro");
                break;
            case "Level 1 Button":
                SceneManager.LoadScene("SwingStage");
                break;
            case "Level 2 Button":
                SceneManager.LoadScene("PullStage");
                break;
            case "Level 3 Button":
                SceneManager.LoadScene("ConnectStage");
                break;
            default:
                Debug.Log("No matching scene!");
                break;
        }
    }
}
