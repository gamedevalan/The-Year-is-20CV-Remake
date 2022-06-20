using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverItem : MonoBehaviour
{
    public Text hoverText;

    public void ShowHoverText()
    {
        if (transform.name.Equals("Health Bottle"))
        {
            hoverText.text = "Apollo's Hope: Increases max health point.";
        }
        if (transform.name.Equals("Attack Bottle"))
        {
            hoverText.text = "Cronus' Fury: Increases max attack stat.";
        }
        if (transform.name.Equals("Defense Bottle"))
        {
            hoverText.text = "Xuanwu's Blessing: Increases max defense stat.";
        }
        if (transform.name.Equals("Crit Damage Bottle"))
        {
            hoverText.text = "Anubis' Wrath: Increases max critical damage stat.";
        }
        if (transform.name.Equals("Crit Rate Charm"))
        {
            hoverText.text = "Excalibur's Percision: Allows you to have a 12% to get a critical hit.";
        }
        if (transform.name.Equals("Heal Charm"))
        {
            hoverText.text = "Caladrius' Down: Increases heal amount by 20% of characters' max health point.";
        }
    }

    public void DefaultHoverText()
    {
        hoverText.text = "Welcome to the shop. What can I do for you?";
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log(transform.name);
    //    if (this.transform.name.Equals("Health Bottle"))
    //    {
    //        hoverText.text = "Bitch";
    //    }
    //}
}
