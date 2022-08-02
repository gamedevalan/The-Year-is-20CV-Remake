using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Set position when going through doors

    // Vertical Directions
    private static Vector3 insideBuilding = new Vector3(0f, 0.5f, -0.95f);
    private static Vector3 insideMansion = new Vector3(0f, 0.5f, -2f);


    private static Vector3 barToOutside = new Vector3(0f, 0.5f, 5.3f);
    private static Vector3 storeToStoreOutside = new Vector3(2.5f, 0.5f, 7.8f);
    private static Vector3 mansionOutsideToOutside = new Vector3(0f, 0.5f, -3.5f);
    private static Vector3 outsideToMansionOutside = new Vector3(0f, 0.5f, 24f);
    private static Vector3 mansionDownstairsToMansionOutside = new Vector3(6.8f, 0.5f, -1.8f);


    // Horizontal Directions
    private static Vector3 outsideToStoreOutside = new Vector3(-5.5f, 0.5f, 1f);
    private static Vector3 storeOutsideToOutside = new Vector3(5.5f, 0.5f, 1f);
    private static Vector3 outsideToBeach = new Vector3(5.5f, 0.5f, 1f);
    private static Vector3 beachToOutside = new Vector3(-5.5f, 0.5f, 1f);

    //public static int costume;
    private static Door instance;
    private static bool doorSoundNeeded;

    private void Start()
    {
        instance = this;
    }

    public static void GoToScene(string location)
    {
        // Case represents where you are going to
        switch (location)
        {
            case "Bar":
            case "Store":
                CharacterMovement.setPos = insideBuilding;
                doorSoundNeeded = true;
                instance.StartCoroutine("WaitForDoorSound", location);
                break;
            case "Mansion_Downstairs":
            case "Mansion_Upstairs":
                if (SceneManager.GetActiveScene().name == "Mansion_Upstairs")
                {
                    CharacterMovement.setPos = new Vector3(0f, 0.5f, 6.5f);
                }
                else
                {
                    CharacterMovement.setPos = insideMansion;
                    if (SceneManager.GetActiveScene().name == "Mansion_Outside") {
                        doorSoundNeeded = true;
                        instance.StartCoroutine("WaitForDoorSound", location);
                    }
                }
                break;
            case "Outside":
                if (SceneManager.GetActiveScene().name == "Bar")
                {
                    CharacterMovement.setPos = barToOutside;
                    doorSoundNeeded = true;
                    instance.StartCoroutine("WaitForDoorSound", location);
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
                    doorSoundNeeded = true;
                    instance.StartCoroutine("WaitForDoorSound", location);
                }
                break;
            case "Beach":
                CharacterMovement.setPos = outsideToBeach;
                break;
            case "Mansion_Outside":
                if (SceneManager.GetActiveScene().name == "Outside")
                {
                    CharacterMovement.setPos = outsideToMansionOutside;
                }
                else if (SceneManager.GetActiveScene().name == "Mansion_Downstairs")
                {
                    CharacterMovement.setPos = mansionDownstairsToMansionOutside;
                    doorSoundNeeded = true;
                    instance.StartCoroutine("WaitForDoorSound", location);
                }
                break;
        }
        if (!doorSoundNeeded) {
            SceneManager.LoadScene(location);
        }
    }

    public static void GoBattle(string enemyType, string scene)
    {
        // Go back to previous position after battle ends
        CharacterMovement.setPos = CharacterMovement.currPos;

        BattleManager.SetEnemy(enemyType);
        GameManager.SetScene(scene);
    }

    IEnumerator WaitForDoorSound(string location)
    {
        Interactables.StopPlayer();
        SoundEffectsOverworld.PlayGoThroughDoor();
        yield return new WaitForSeconds(0.75f);
        Interactables.StartPlayer();
        doorSoundNeeded = false;
        SceneManager.LoadScene(location);
    }
}
