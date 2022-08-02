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
            hoverText.text = "Apollo's Hope: Increases max health point. Cost: $" + ShopManager.healthPotionCost + ". " + ShopManager.healthPotions + " left.";
        }
        if (transform.name.Equals("Attack Bottle"))
        {
            hoverText.text = "Cronus' Fury: Increases max attack stat. Cost: $" + ShopManager.attackPotionCost + ". " + ShopManager.attackPotions + " left.";
        }
        if (transform.name.Equals("Defense Bottle"))
        {
            hoverText.text = "Xuanwu's Blessing: Increases max defense stat. Cost: $" + ShopManager.defensePotionCost + ". " + ShopManager.defensePotions + " left.";
        }
        if (transform.name.Equals("Crit Damage Bottle"))
        {
            hoverText.text = "Anubis' Wrath: Increases max critical damage stat. Cost: $" + ShopManager.critDamagePotionCost + ". " + ShopManager.critDamagePotions + " left.";
        }
        if (transform.name.Equals("Crit Rate Charm"))
        {
            hoverText.text = "Excalibur's Percision: Allows you to never miss and have a 25% chance to get a critical hit. Cost: $" + ShopManager.critRateCharmCost;
        }
        if (transform.name.Equals("Heal Charm"))
        {
            hoverText.text = "Caladrius' Heart: Increases heal amount by 20% of Anna's max health point. Cost: $" + ShopManager.healCharmCost;
        }
    }

    public void DefaultHoverText()
    {
        hoverText.text = "Welcome to the bush shop. What can I do for you?";
    }
}
