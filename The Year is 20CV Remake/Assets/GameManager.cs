using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<int, bool> itemsInGame = new Dictionary<int, bool>();

    private static int totalBossesLeft = 4;
    public GameObject leftBarrierDoor;
    public GameObject downBarrierDoor;

    public GameObject barrierL;
    public GameObject barrierD;

    public GameObject endDoor;

    // save the position of character before going to menu
    public static Vector3 position;

    public static int costume;
    public static string lastScene = "Start Scene";

    private void Update()
    {
        if (totalBossesLeft < 4)
        {
            leftBarrierDoor.SetActive(true);
            barrierL.SetActive(false);
        }
        if (totalBossesLeft < 3)
        {
            downBarrierDoor.SetActive(true);
            barrierD.SetActive(false);
        }

        if (totalBossesLeft == 0)
        {
            endDoor.gameObject.tag = "End";
        }

        if (totalBossesLeft == -1)
        {
            endDoor.gameObject.tag = "Bar";
        }
    }

    // Go back to what the last scene was.
    public static void SetScene(string scene)
    {
        lastScene = scene;
    }

    public static int GetNumBosses()
    {
        return totalBossesLeft;
    }

    public static void OpenNewEnvironment()
    {
        totalBossesLeft--;
    }
}
