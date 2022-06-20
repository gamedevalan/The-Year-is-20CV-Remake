using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<int, bool> itemsInGame = new Dictionary<int, bool>(); 

    //[SerializeField]
    private int totalBossesLeft = 4;
    public GameObject leftBarrierDoor;
    public GameObject downBarrierDoor;

    public GameObject barrierL;
    public GameObject barrierD;


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
    }
}
