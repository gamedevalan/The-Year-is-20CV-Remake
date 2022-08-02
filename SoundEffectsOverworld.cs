using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsOverworld : MonoBehaviour
{
    public static float volume = 1f;

    private static SoundEffectsOverworld instance;

    public AudioSource pickUp;
    private static AudioSource pickUpStatic;

    public AudioSource door;
    private static AudioSource doorStatic;

    public AudioSource drinkPowerup;
    private static AudioSource drinkPowerupStatic;

    public AudioSource buyItem;
    private static AudioSource buyItemStatic;

    public GameObject DoorTransition;

    private void Start()
    {
        instance = this;
        pickUpStatic = pickUp;
        doorStatic = door;
        drinkPowerupStatic = drinkPowerup;
        buyItemStatic = buyItem;
    }

    public static void PlayPickUpSound()
    {
        pickUpStatic.Play();
    }

    public static void PlayDrinkSound()
    {
        drinkPowerupStatic.Play();
    }

    public static void BuyItemSound()
    {
        buyItemStatic.Play();
    }

    public static void PlayGoThroughDoor()
    {
        instance.DoorTransition.SetActive(true);
        doorStatic.Play();
    }

    private void Update()
    {
        pickUp.volume *= volume;
        door.volume = volume;
        drinkPowerup.volume = volume;
        buyItem.volume = volume;
    }
}
