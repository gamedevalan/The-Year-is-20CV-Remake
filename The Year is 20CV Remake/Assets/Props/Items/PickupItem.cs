using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool pickedUp;
    public int uniqueKey;
    public bool keyItem;

    private void Awake()
    {
        // Adds items to a dictionary at loadtime.
        // See if the item should be loaded into the game when scene is loaded.
        // If not picked up, keep it shown.
        if (!GameManager.itemsInGame.ContainsKey(uniqueKey))
        {
            GameManager.itemsInGame.Add(uniqueKey, false);
        }
        else
        {
            this.pickedUp = GameManager.itemsInGame[uniqueKey];
        }
        if (pickedUp) {
            this.GetComponent<Rigidbody>().detectCollisions = false;
            this.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetPickedUp()
    {
        this.pickedUp = true;
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.itemsInGame[uniqueKey] = true;
        if (keyItem)
        {
            KeyitemFound();
        }
    }

    private void KeyitemFound()
    {
        GameManager.OpenNewEnvironment();
    }
}
