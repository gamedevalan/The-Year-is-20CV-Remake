using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Dictionary<string, int> inventory = new Dictionary<string, int>();

    // 9 items in the game.
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] itemPrefabs;

    private readonly static float defaultMoney = 500;
    public static float currMoney = defaultMoney;
    public Text inspectorMoneyInBag;
    private static Text moneyInBag;

    private static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //currMoney = defaultMoney;
        moneyInBag = inspectorMoneyInBag;
    }

    public static void AddToInventory(string obj)
    {
        if (!inventory.ContainsKey(obj))
        {
            inventory.Add(obj, 1);
        }
        else
        {
            inventory[obj] = inventory[obj] + 1;
        }
    }

    public static void RemoveFromInventory(string obj, Transform currItem)
    {
        if (inventory[obj] > 1)
        {
            inventory[obj] = inventory[obj] - 1;
            currItem.parent.GetChild(0).GetComponent<Text>().text = "X" + inventory[obj];
        }
        else
        {
            inventory.Remove(obj);
            Destroy(currItem.parent.gameObject);
        }
    }

    // Maybe make cleaner?
    public static void ShowInventory()
    {
        if (currMoney < 999999999)
        {
            moneyInBag.text = "$" + currMoney;
        }
        else
        {
            moneyInBag.text = "1 Billion";
        }

        if (inventory.Count > 0)
        {
            // Go through all the items that has been picked up.
            foreach(var item in inventory)
            {
                // Go through all slots.
                for (int i = 0; i < instance.isFull.Length; i++)
                {

                    if (!instance.isFull[i])
                    {
                        instance.isFull[i] = true;
                        GameObject currItem;
                        // Find the correct prefab item.
                        for (int j = 0; j < instance.itemPrefabs.Length; j++)
                        {
                            if (instance.itemPrefabs[j].name.Equals(item.Key))
                            {
                                if (instance.slots[i].gameObject.transform.childCount < 1)
                                {
                                    currItem = instance.itemPrefabs[j];
                                    currItem.transform.GetChild(0).GetComponent<Text>().text = "X" + item.Value;
                                    Instantiate(currItem, instance.slots[i].gameObject.transform, false);
                                }
                                else
                                {
                                    // Update number of a certain item
                                    instance.slots[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + item.Value; ;
                                }
                                break;
                            }
                        }
                        //instance.PutItemInSlot(item, i);
                        break;
                    }
                }
            }
        }
    }

    // Called when bag is closed
    public static void SetAllToFalse()
    {
        for (int i = 0; i < instance.isFull.Length; i++)
        {
            instance.isFull[i] = false;
            // Maybe Bad idea but also good
            if (instance.slots[i].transform.childCount > 0)
            {
                Destroy(instance.slots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    //private void PutItemInSlot(KeyValuePair<string, int> item, int i)
    //{
    //    GameObject currItem;
    //    // Find the correct prefab item.
    //    for (int j = 0; j < instance.itemPrefabs.Length; j++)
    //    {
    //        if (instance.itemPrefabs[j].name.Equals(item.Key))
    //        {
    //            currItem = instance.itemPrefabs[j];
    //            Instantiate(currItem, instance.slots[i].gameObject.transform, false);
    //            currItem.transform.GetChild(0).GetComponent<Text>().text = "X" + item.Value;
    //            break;
    //        }
    //    }
    //}

}
