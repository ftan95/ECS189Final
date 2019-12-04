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
            case "Level 1 Button":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Level 2 Button":
                SceneManager.LoadScene("Demo");
                break;
            default:
                Debug.Log("No matching scene!");
                break;
        }
    }
}
