using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public int index;
    private readonly float typingSpeed = 0.05f;

    public GameObject nextButton;

    //public GameObject bag;
    public GameObject pressSpacePrompt;
    public GameObject player;

    private void OnEnable()
    {
        if (BagManager.bag != null)
        {
            BagManager.HideBagIcon();
        }
        if (pressSpacePrompt != null)
        {
            pressSpacePrompt.SetActive(false);
        }
        Interactables.StopPlayer();
        textDisplay.text = "";
        nextButton.SetActive(false);
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
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            if (BagManager.bag != null)
            {
                BagManager.ShowBagIcon();
            }
            if (pressSpacePrompt != true)
            {
                pressSpacePrompt.SetActive(true);
            }
            Interactables.StartPlayer();
            index = 0;
            textDisplay.text = "";
            transform.gameObject.SetActive(false);
        }
    }
}