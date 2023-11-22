using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
{
    public GameObject inspectorText;
    public static GameObject textBox;

    public GameObject inspectorShop;
    public static GameObject shop;

    public GameObject inspectorPlayer;
    private static GameObject player;

    public static bool bossDialogue;
    public static string currBoss;

    private void Start()
    {
        player = inspectorPlayer;
        shop = inspectorShop;
        textBox = inspectorText;
    }

    public static void ShowDialogue(string word, string extra)
    {
        switch (word)
        {
            case "Chair":
                bossDialogue = false;
                textBox.SetActive(true);
                break;
            case "Door":
                Door.GoToScene(extra);
                break;
            case "Bottle":
            case "KeyItem":
                SoundEffectsOverworld.PlayPickUpSound();
                Inventory.AddToInventory(extra);
                break;
            case "SignL":
            case "SignD":
                ShowSign.ShowSigns(word);
                break;
            case "Shop":
                shop.SetActive(true);
                StopPlayer();
                BagManager.HideBagIcon();
                break;
            case "Karen":
            case "Spring Breaker":
            case "Vampire":
            case "Virus-Lrg":
                bossDialogue = true;
                textBox.SetActive(true);
                currBoss = word;
                break;
            case "End":
                GameManager.lastScene = "Bar";
                SceneManager.LoadScene("End Scene");
                break;
            default: // Enemies
                Door.GoBattle(word, SceneManager.GetActiveScene().name);
                break;
        }
    }

    public static void HideShop()
    {
        shop.SetActive(false);
        StartPlayer();
        BagManager.ShowBagIcon();
    }

    // Allows player to move
    public static void StartPlayer()
    {
        player.GetComponent<Rigidbody>().detectCollisions = true;
        player.GetComponent<CharacterMovement>().enabled = true;
    }

    // Don't allow player to move
    public static void StopPlayer()
    {
        player.GetComponent<Rigidbody>().detectCollisions = false;
        player.GetComponent<CharacterMovement>().enabled = false;
    }
}
