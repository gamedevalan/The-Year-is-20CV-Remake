using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Set position when going through doors

    // Vertical Directions
    private static Vector3 outsideToBar = new Vector3(0f, 0.5f, -0.95f);
    private static Vector3 barToOutside = new Vector3(0f, 0.5f, 5.3f);
    private static Vector3 storeOutsideToStore = new Vector3(0f, 0.5f, -0.95f);
    private static Vector3 storeToStoreOutside = new Vector3(2.5f, 0.5f, 7.7f);
    private static Vector3 mansionOutsideToOutside = new Vector3(0f, 0.5f, -3.5f);
    private static Vector3 outsideToMansionOutside = new Vector3(0f, 0.5f, 24f);


    // Horizontal Directions
    private static Vector3 outsideToStoreOutside = new Vector3(-5.5f, 0.5f, 1f);
    private static Vector3 storeOutsideToOutside = new Vector3(5.5f, 0.5f, 1f);
    private static Vector3 outsideToBeach = new Vector3(5.5f, 0.5f, 1f);
    private static Vector3 beachToOutside = new Vector3(-5.5f, 0.5f, 1f);

    public static int costume;

    public static void GoToScene(string location)
    {
        // Case represents where you are going to
        switch (location)
        {
            case "Bar":
                CharacterMovement.setPos = outsideToBar;
                break;
            case "Outside":
                if (SceneManager.GetActiveScene().name == "Bar")
                {
                    CharacterMovement.setPos = barToOutside;
                }
                else if(SceneManager.GetActiveScene().name == "Store_Outside")
                {
                    CharacterMovement.setPos = storeOutsideToOutside;
                }
                else if (SceneManager.GetActiveScene().name == "Beach")
                {
                    CharacterMovement.setPos = beachToOutside;
                }
                else if (SceneManager.GetActiveScene().name == "Mansion_Outside")
                {
                   CharacterMovement.setPos = mansionOutsideToOutside;
                }
                break;
            case "Store_Outside":
                if (SceneManager.GetActiveScene().name == "Outside")
                {
                    CharacterMovement.setPos = outsideToStoreOutside;
                }
                else
                {
                    CharacterMovement.setPos = storeToStoreOutside;
                }
                break;
            case "Store":
                CharacterMovement.setPos = storeOutsideToStore;
                break;
            case "Beach":
                CharacterMovement.setPos = outsideToBeach;
                break;
            case "Mansion_Outside":
                CharacterMovement.setPos = outsideToMansionOutside;
                break;
        }
        SceneManager.LoadScene(location);
    }

    public static void GoBattle(string enemyType, string scene)
    {
        CharacterMovement.setPos = CharacterMovement.currPos;
        BattleManager.SetEnemy(enemyType);
        BattleManager.SetScene(scene);
        //SceneManager.LoadScene("Battle");
    }
}
