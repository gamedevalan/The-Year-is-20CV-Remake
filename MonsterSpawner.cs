using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Random.Range() is inclu to exclu.
        GameObject temp;
        int x = 0;
        int z = 0;
        int num = Random.Range(0,2);
        if (SceneManager.GetActiveScene().name == "Outside" && GameManager.GetNumBosses() != 1)
        {
            x = Random.Range(1, 6);
            z = Random.Range(-3, 1);
            temp = Instantiate(enemiesToSpawn[0], new Vector3(x, 0.5f, z), Quaternion.identity);
            if (num == 1)
            {
                temp.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            }
        }

        else if (SceneManager.GetActiveScene().name == "Store_Outside")
        {
            x = Random.Range(-4, 6);
            z = Random.Range(-2, 6);
            temp = Instantiate(enemiesToSpawn[0], new Vector3(x, 0.5f, z), Quaternion.identity);
            if (num == 1)
            {
                temp.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            }
        }


        else if (SceneManager.GetActiveScene().name == "Mansion_Outside")
        {
            for(int i =0; i < 3; i++)
            {
                num = Random.Range(0, 2);
                x = Random.Range(-10, 11);
                z = Random.Range(14, 26);
                Vector3 pos = new Vector3(x, 0.5f, z);
                temp = Instantiate(enemiesToSpawn[1], pos, Quaternion.identity);
                if (num == 1)
                {
                    Vector3 tempPos = temp.transform.GetChild(0).localScale;
                    temp.transform.GetChild(0).localScale = new Vector3(-tempPos.x, tempPos.y, tempPos.z);
                }
            }
        }

        else if (SceneManager.GetActiveScene().name == "Beach")
        {
            for (int i = 0; i < 2; i++)
            {
                num = Random.Range(0, 2);
                x = Random.Range(-4, 6);
                z = Random.Range(-3, 1);
                Vector3 pos = new Vector3(x, 0.5f, z);
                temp = Instantiate(enemiesToSpawn[2], pos, Quaternion.identity);
                if (num == 1)
                {
                    temp.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }
}
