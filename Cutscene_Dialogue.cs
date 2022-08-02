using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene_Dialogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public int index;
    private float typingSpeed = 0.0001f;
    public GameObject[] whosTalking;
    public GameObject nextButton;

    private void OnEnable()
    {
        textDisplay.text = "";
        nextButton.SetActive(false);

        ShowDialogueSprites();
        StartCoroutine(Type());
    }

    private void Update()
    {
        // Only show next button once current paragraph finishes.
        if (textDisplay.text == sentences[index])
        {
            nextButton.SetActive(true);
        }
    }

    IEnumerator PauseBeforeNext()
    {
        yield return new WaitForSeconds(5f);
        NextSentence();
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            // Type writer text display.
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Goes to the next sentene talking if applicable, if not, hides the text box.
    public void NextSentence()
    {
        nextButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            nextButton.SetActive(false);
            index++;
            textDisplay.text = "";

            ShowDialogueSprites();
            
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            textDisplay.text = "";
            if (SceneManager.GetActiveScene().name == "Start Scene")
            {
                SceneManager.LoadScene("Bar");
            }
            else
            {
                GameManager.OpenNewEnvironment();
                Menu.endingScreenShow = true;
                SceneManager.LoadScene("End");
            }
        }
    }

    public void ShowDialogueSprites()
    {
        GameObject currChar = whosTalking[index];
        whosTalking[index].SetActive(true);
        for (int i = 0; i < whosTalking.Length; i++)
        {
            if (currChar != whosTalking[i])
            {
                whosTalking[i].SetActive(false);
            }
        }
    }
}