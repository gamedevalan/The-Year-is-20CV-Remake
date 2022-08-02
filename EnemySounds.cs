using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public void EnemyHitSound()
    {
        SoundEffectsBattle.enemyHitStatic.Play();
    }

    public void PowerUpSound()
    {
        SoundEffectsBattle.powerUpStatic.Play();
    }

    public void DrinkSound()
    {
        SoundEffectsBattle.drinkStatic.Play();
    }

    public void EarthquakeSound()
    {
        SoundEffectsBattle.earthquakeStatic.Play();
    }

    public void MultiHitsSound()
    {
        // 8 beats
        SoundEffectsBattle.multiHitsStatic.Play();
    }

    public void ClawSound()
    {
        SoundEffectsBattle.clawStatic.Play();
    }

    public void AbsorbSound()
    {
        SoundEffectsBattle.vampAbsorbStatic.Play();
    }

    public void SnapSound()
    {
        SoundEffectsBattle.snapStatic.Play();
    }

    public void WhooshSound()
    {
        SoundEffectsBattle.whooshStatic.Play();
    }

    public void BiteSound()
    {
        SoundEffectsBattle.biteStatic.Play();
    }

    public void SandHitSound()
    {
        SoundEffectsBattle.sandHitStatic.Play();
    }
}
