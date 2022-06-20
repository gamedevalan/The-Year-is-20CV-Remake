using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    private static int healthPotions = 5;
    private static int attackPotions = 8;
    private static int defensePotions = 5;
    private static int critDamagePotions = 2;
    private static int critRateCharm = 1;
    private static int healCharm = 1;

    private int healthPotionCost = 50;
    private int attackPotionCost = 50;
    private int defensePotionCost = 50;
    private int critDamagePotionCost = 50;
    private int critRateCharmCost = 200;
    private int healCharmCost = 150;


    public Button hButton;
    public Button aButton;
    public Button dButton;
    public Button cdButton;
    public Button crButton;
    public Button healButton;

    public Text moneyInBag;

    private void Start()
    {
        //if (healthPotions <= 0)
        //{
        //    hButton.SetActive(false);
        //}
        //if (attackPotions <= 0)
        //{
        //    aButton.SetActive(false);
        //}
        //if (defensePotions <= 0)
        //{
        //    dButton.SetActive(false);
        //}
        //if (critDamagePotions <= 0)
        //{
        //    cdButton.SetActive(false);
        //}
    }

    private void Update()
    {
        ShowMoney();
        if (healthPotions <= 0 || Inventory.currMoney < healthPotionCost)
        {
            SetButtonCannotBuy(hButton, healthPotions);
        }
        else
        {
            SetButtonToBuy(hButton, healthPotions);
        }

        if (attackPotions <= 0 || Inventory.currMoney < attackPotionCost)
        {
            SetButtonCannotBuy(aButton, attackPotions);

        }
        else
        {
            SetButtonToBuy(aButton, attackPotions);
        }

        if (defensePotions <= 0 || Inventory.currMoney < defensePotionCost)
        {
            SetButtonCannotBuy(dButton, defensePotions);

        }
        else
        {
            SetButtonToBuy(dButton, defensePotions);
        }

        if (critDamagePotions <= 0 || Inventory.currMoney < critDamagePotionCost)
        {
            SetButtonCannotBuy(cdButton, critDamagePotions);

        }
        else
        {
            SetButtonToBuy(cdButton, critDamagePotions);
        }

        if (critRateCharm <= 0 || Inventory.currMoney < critRateCharmCost)
        {
            SetButtonCannotBuy(crButton, critRateCharm);

        }
        else
        {
            SetButtonToBuy(crButton, critRateCharm);
        }

        if (healCharm <= 0 || Inventory.currMoney < healCharmCost)
        {
            SetButtonCannotBuy(healButton, healCharm);

        }
        else
        {
            SetButtonToBuy(healButton, healCharm);
        }
    }

    public void SetButtonCannotBuy(Button button, int numPotions)
    {
        button.interactable = false;
        if (numPotions <= 0)
        {
            button.gameObject.transform.GetComponentInChildren<Text>().text = "Sold Out";
        }
        else
        {
            button.gameObject.transform.GetComponentInChildren<Text>().text = "No Funds";
        }
        button.gameObject.transform.GetComponentInChildren<Text>().fontSize = 25;
    }

    public void SetButtonToBuy(Button button, int numPotions)
    {
        button.interactable = true;
        button.gameObject.transform.GetComponentInChildren<Text>().text = "Buy";
        button.gameObject.transform.GetComponentInChildren<Text>().fontSize = 30;
    }


    /*Rewrite all to look better*/
    public void BuyHealth()
    {
        healthPotions--;
        Inventory.currMoney -= healthPotionCost;
        Inventory.AddToInventory("Health Bottle");
    }

    public void BuyAttack()
    {
        attackPotions--;
        Inventory.currMoney -= attackPotionCost;
        Inventory.AddToInventory("Attack Bottle");
    }

    public void BuyDefense()
    {
        defensePotions--;
        Inventory.currMoney -= defensePotionCost;
        Inventory.AddToInventory("Defense Bottle");
    }

    public void BuyCritDamage()
    {
        critDamagePotions--;
        Inventory.currMoney -= critDamagePotionCost;
        Inventory.AddToInventory("Crit Damage Bottle");
    }

    public void BuyCRCharm()
    {
        critRateCharm--;
        Inventory.currMoney -= critRateCharmCost;
        Inventory.AddToInventory("Crit Rate Charm");
        CharacterMovement.critRateCharmBought = true;
    }

    public void BuyHealCharm()
    {
        healCharm--;
        Inventory.currMoney -= healCharmCost;
        Inventory.AddToInventory("Heal Charm");
        CharacterMovement.healCharmBought = true;
    }

    public void ExitStore()
    {
        Interactables.HideShop();
    }

    private void ShowMoney()
    {
        if (Inventory.currMoney < 999999999)
        {
            moneyInBag.text = "$" + Inventory.currMoney;
        }
        else
        {
            moneyInBag.text = "1 Billion";
        }
    }
}
