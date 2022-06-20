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
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            float x = Random.Range(1, 6);
            float z = Random.Range(-3, 0);
            Instantiate(enemiesToSpawn[0], new Vector3(x, 0.5f, z), Quaternion.identity);
        }

        else if (SceneManager.GetActiveScene().name == "Store_Outside")
        {
            float x = Random.Range(-4, 5);
            float z = Random.Range(-2, 5);
            Instantiate(enemiesToSpawn[0], new Vector3(x, 0.5f, z), Quaternion.identity);
        }
    }
}
