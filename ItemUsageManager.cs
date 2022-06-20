using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script is used by the item prefabs that are put into the bag/ inventory*/
public class ItemUsageManager : MonoBehaviour
{
    public void UseBottle()
    {
        Debug.Log(transform.name);
        string name = transform.parent.name.Replace("(Clone)", "");
        Inventory.RemoveFromInventory(name, transform);
        BagManager.ChangePlayerStats(name);
    }

}
