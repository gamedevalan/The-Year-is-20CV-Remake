using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsBattle : MonoBehaviour
{
    public static float volume = 1f;
    public AudioSource hurtNormal;
    public static AudioSource hurtNormalStatic;

    public AudioSource hurtCritical;
    public static AudioSource hurtCriticalStatic;


    // Player Sounds
    public AudioSource swordSlash;
    public static AudioSource swordSlashStatic;

    public AudioSource fireExplosion;
    public static AudioSource fireExplosionStatic;

    public AudioSource fireCharge;
    public static AudioSource fireChargeStatic;

    public AudioSource heal;
    public static AudioSource healStatic;

    public AudioSource shield;
    public static AudioSource shieldStatic;

    public AudioSource provoke;
    public static AudioSource provokeStatic;

    public AudioSource whoosh;
    public static AudioSource whooshStatic;

    // Enemy Sounds

    public AudioSource poisonGas;
    public static AudioSource poisonGasStatic;

    public AudioSource enemyHit;
    public static AudioSource enemyHitStatic;

    public AudioSource powerUp;
    public static AudioSource powerUpStatic;

    public AudioSource drink;
    public static AudioSource drinkStatic;

    public AudioSource earthquake;
    public static AudioSource earthquakeStatic;

    public AudioSource multiHits;
    public static AudioSource multiHitsStatic;

    public AudioSource claw;
    public static AudioSource clawStatic;

    public AudioSource vampAbsorb;
    public static AudioSource vampAbsorbStatic;

    public AudioSource snap;
    public static AudioSource snapStatic;

    public AudioSource curse;
    public static AudioSource curseStatic;

    public AudioSource bite;
    public static AudioSource biteStatic;

    public AudioSource sandHit;
    public static AudioSource sandHitStatic;


    private void Start()
    {
        swordSlashStatic = swordSlash;
        fireExplosionStatic = fireExplosion;
        fireChargeStatic = fireCharge;
        healStatic = heal;
        shieldStatic = shield;
        provokeStatic = provoke;

        poisonGasStatic = poisonGas;
        hurtNormalStatic = hurtNormal;
        hurtCriticalStatic = hurtCritical;

        powerUpStatic = powerUp;
        enemyHitStatic = enemyHit;
        drinkStatic = drink;
        earthquakeStatic = earthquake;
        whooshStatic = whoosh;
        multiHitsStatic = multiHits;
        clawStatic = claw;
        vampAbsorbStatic = vampAbsorb;
        snapStatic = snap;
        curseStatic = curse;
        biteStatic = bite;
        sandHitStatic = sandHit;
    }

    private void Update()
    {
        swordSlash.volume *= volume;
        fireExplosion.volume *= volume;
        fireCharge.volume *= volume;
        heal.volume *= volume;
        shield.volume *= volume;
        provoke.volume *= volume;

        poisonGas.volume *= volume;
        hurtNormal.volume *= volume;
        hurtCritical.volume *= volume;

        powerUp.volume *= volume;
        enemyHit.volume *= volume;
        drink.volume *= volume;
        earthquake.volume *= volume;
        whoosh.volume *= volume;
        multiHits.volume *= volume;
        claw.volume *= volume;
        vampAbsorb.volume *= volume;
        snap.volume *= volume;
        curse.volume *= volume;
        bite.volume *= volume;
        sandHit.volume *= volume;
    }
}
