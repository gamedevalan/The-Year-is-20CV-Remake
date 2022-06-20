using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    public GameObject insideBag;
    public GameObject inspectorBag;
    public static GameObject bag;

    private static BagManager instance;
    private string stat;

    public Text taiStatsShow;
    public Text annaStatsShow;

    public GameObject statSummary;
    public GameObject taiButton;
    public GameObject annaButton;

    public Text title;

    public GameObject exitSummaryButton;

    private void Start()
    {
        instance = this;
        bag = inspectorBag;
        insideBag.SetActive(false);    
    }

    public void OpenBag()
    {
        insideBag.SetActive(true);
        Interactables.StopPlayer();
        Inventory.ShowInventory();
        statSummary.SetActive(false);
        instance.taiButton.SetActive(false);
        instance.annaButton.SetActive(false);
    }

    public void CloseBag()
    {
        Inventory.SetAllToFalse();
        insideBag.SetActive(false);
        Interactables.StartPlayer();
    }

    public static void HideBagIcon()
    {
        bag.SetActive(false);
    }

    public static void ShowBagIcon()
    {
        bag.SetActive(true);
    }

    public void TaiChoseToChange()
    {
        if (stat.Equals("Health Bottle"))
        {
            CharacterMovement.taiHealth += 10;
        }
        else if (stat.Equals("Attack Bottle"))
        {
            CharacterMovement.taiAttack += 5;
        }
        else if (stat.Equals("Defense Bottle"))
        {
            CharacterMovement.taiDefense += 10;
        }

        else if (stat.Equals("Crit Damage Bottle"))
        {
            CharacterMovement.taiCritDamMultiplier += .15f;
        }
        instance.taiButton.SetActive(false);
        instance.annaButton.SetActive(false);
        statSummary.SetActive(false);
    }

    public void AnnaChoseToChange()
    {
        if (stat.Equals("Health Bottle"))
        {
            CharacterMovement.annaHealth += 10;
        }
        else if (stat.Equals("Attack Bottle"))
        {
            CharacterMovement.annaAttack += 5;
        }
        else if (stat.Equals("Defense Bottle"))
        {
            CharacterMovement.annaDefense += 10;
        }

        else if (stat.Equals("Crit Damage Bottle"))
        {
            CharacterMovement.annaCritDamMultiplier += .15f;
        }
        instance.taiButton.SetActive(false);
        instance.annaButton.SetActive(false);
        statSummary.SetActive(false);
    }

    // Shows the Summary panel and the buttons to choose.
    public static void ChangePlayerStats(string charStat)
    {
        instance.stat = charStat;
        instance.statSummary.SetActive(true);
        instance.taiButton.SetActive(true);
        instance.annaButton.SetActive(true);
        instance.exitSummaryButton.SetActive(false);
        ShowSummary("Who would you like to give this too?");
    }

    // Shows the Summary panel but not the buttons.
    public void SummaryOfStats()
    {
        instance.statSummary.SetActive(true);
        instance.exitSummaryButton.SetActive(true);
        ShowSummary("Stat Summary");
    }

    public static void ShowSummary(string sentence)
    {
        instance.title.text = sentence;
        instance.taiStatsShow.text =
            "Tai\n\n"+
            "Health: " + CharacterMovement.taiHealth + "\n\n" +
            "Attack: " + CharacterMovement.taiAttack + "\n\n" +
            "Defense: " + CharacterMovement.taiDefense + "\n\n" +
            "Critical Damage Multiplier: " + CharacterMovement.taiCritDamMultiplier;

        instance.annaStatsShow.text =
            "Anna\n\n" +
            "Health: " + CharacterMovement.annaHealth + "\n\n" +
            "Attack: " + CharacterMovement.annaAttack + "\n\n" +
            "Defense: " + CharacterMovement.annaDefense + "\n\n" +
            "Critical Damage Multiplier: " + CharacterMovement.annaCritDamMultiplier;
    }

    // Hides to Summary panel but stays inside the bag.
    public void ExitSummary()
    {
        instance.statSummary.SetActive(false);
    }
}
