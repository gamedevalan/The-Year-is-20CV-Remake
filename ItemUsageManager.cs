using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script is used by the item prefabs that are put into the bag/ inventory*/
public class ItemUsageManager : MonoBehaviour
{
    public static string nameOfBottle;
    public void UseBottle()
    {
        string name = transform.parent.name.Replace("(Clone)", "");
        nameOfBottle = name;
        Inventory.RemoveFromInventory(name, transform);
        BagManager.ChangePlayerStats(name);
    }

}
