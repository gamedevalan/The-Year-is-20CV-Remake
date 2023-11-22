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
    private float typingSpeed = 0.0001f;
    public GameObject[] whosTalking;
    public GameObject cutScenes;


    public GameObject nextButton;
    public GameObject skipButton;

    public GameObject pressSpacePrompt;
    public GameObject player;

    private void OnEnable()
    {
        if (Interactables.bossDialogue)
        {
            skipButton.SetActive(true);
        }
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
        if (cutScenes != null)
        {
            cutScenes.SetActive(true);
            ShowDialogueSprites();
        }
        StartCoroutine(Type());
    }

    private void Update()
    {
        // Only show next button once current paragraph finishes.
        if (textDisplay.text == sentences[index])
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    nextButton.SetActive(false);
            //    NextSentence();
            //}
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
            if (cutScenes != null) {
                ShowDialogueSprites();
            }
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
            if (Interactables.bossDialogue)
            {
                Door.GoBattle(Interactables.currBoss, SceneManager.GetActiveScene().name);
            }
            else
            {
                cutScenes.SetActive(false);
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

    public void SkipDialogue()
    {
        Door.GoBattle(Interactables.currBoss, SceneManager.GetActiveScene().name);
    }
}