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

    private void Start()
    {
        player = inspectorPlayer;
        shop = inspectorShop;
        textBox = inspectorText;
        //DontDestroyOnLoad(transform);
    }

    public static void ShowDialogue(string word, string extra)
    {
        switch (word)
        {
            case "Chair":
                textBox.SetActive(true);
                break;
            case "Door":
                Door.GoToScene(extra);
                break;
            case "Bottle":
                Inventory.AddToInventory(extra);
                break;
            case "SignL":
            case "SignD":
                ShowSign.ShowSigns(word);
                break;
            case "Virus-SM":
            case "Bat":
                Door.GoBattle(word, SceneManager.GetActiveScene().name);
                //BattleManager.SetEnemy(word);
                break;
            case "Shop":
                shop.SetActive(true);
                StopPlayer();
                BagManager.HideBagIcon();
                break;
            default:
                break;
        }
    }

    public static void HideShop()
    {
        shop.SetActive(false);
        StartPlayer();
        BagManager.ShowBagIcon();
    }

    public static void StartPlayer()
    {
        player.GetComponent<Rigidbody>().detectCollisions = true;
        player.GetComponent<CharacterMovement>().enabled = true;
    }

    public static void StopPlayer()
    {
        player.GetComponent<Rigidbody>().detectCollisions = false;
        player.GetComponent<CharacterMovement>().enabled = false;
    }
}
