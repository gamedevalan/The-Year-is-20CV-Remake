using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnBosses : MonoBehaviour
{
    public GameObject[] Bosses;
    public static bool[] bossDefeated = new bool[4];
    public static int currBoss;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Store" && bossDefeated[0] == false)
        {
            Instantiate(Bosses[0]);
        }
        if (SceneManager.GetActiveScene().name == "Beach" && bossDefeated[1] == false)
        {
            Instantiate(Bosses[1]);
        }
        if (SceneManager.GetActiveScene().name == "Mansion_Upstairs" && bossDefeated[2] == false)
        {
            Instantiate(Bosses[2]);
        }
        if (SceneManager.GetActiveScene().name == "Outside" && bossDefeated[3] == false && GameManager.GetNumBosses() == 1)
        {
            Instantiate(Bosses[3]);
        }
    }

    public static void BeatBoss()
    {
        bossDefeated[currBoss] = true;
    }
}
