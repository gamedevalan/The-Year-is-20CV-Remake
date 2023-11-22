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

    private readonly static float defaultMoney = 100;
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

    public static void ShowInventory()
    {
        if (currMoney <= 999999999)
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
                                    if (currItem.transform.childCount != 0)
                                    {
                                        currItem.transform.GetChild(0).GetComponent<Text>().text = "X" + item.Value;
                                    }
                                    Instantiate(currItem, instance.slots[i].gameObject.transform, false);
                                }
                                else
                                {
                                    // Update number of a certain item
                                    if (instance.slots[i].transform.GetChild(0).childCount != 0)
                                    {
                                        instance.slots[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + item.Value;
                                    }
                                }
                                break;
                            }
                        }
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
            if (instance.slots[i].transform.childCount > 0)
            {
                Destroy(instance.slots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public static void ChangeMoney(int money)
    {
        currMoney += money;
        if (currMoney > 999999999)
        {
            currMoney = 999999999;
        }
        if (currMoney < 0)
        {
            currMoney = 0;
        }
    }
}
