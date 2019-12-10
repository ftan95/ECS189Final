using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private float LetterDelay = 0.1f;
    [SerializeField] private float SentenceDelay = 0.5f;

    private string StoryText = "This is Henry. He's the best delivery man in the land of Crath. The roads in Crath have been destroyed, and it is Henry's job to deliver the supplies needed to fix them to King Alexander’s palace. In the meantime, he must use his trusty grappling hook to make his way to the palace. Quick thinking and tight reflexes will ensure his success and the prosperity of all in Crath.";
    private Text TextBox;
    private Text ContinueText;
    private bool TextDone = false;

    void Start()
    {
        ContinueText = GameObject.Find("ContinueText").GetComponent<Text>();
        ContinueText.enabled = false;
        TextBox = GameObject.Find("StoryText").GetComponent<Text>();
        TextBox.text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        foreach (var letter in StoryText)
        {
            TextBox.text += letter;
            if ('.' == letter)
            {
                yield return new WaitForSeconds(SentenceDelay);
            }
            else
            {
                yield return new WaitForSeconds(LetterDelay);
            }
        }
        EndText();
    }

    private void EndText()
    {
        StopAllCoroutines();
        TextDone = true;
        ContinueText.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!TextDone)
            {
                TextBox.text = StoryText;
                EndText();
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        
    }
}
