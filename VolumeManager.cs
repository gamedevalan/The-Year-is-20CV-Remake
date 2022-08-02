using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static float volumeLevelMusic = 1f;
    private static float volumeLevelSFX = 1f;
    public Slider slideMusic;
    public Slider slideSFX;

    public Text musicNumber;
    public Text SFXNumber;

    public static SoundEffectsOverworld instanceOverworld;
    public static SoundEffectsBattle instanceBattle;

    private void Start()
    {
        slideMusic.value = volumeLevelMusic;
        slideSFX.value = volumeLevelSFX;
    }

    private void Update()
    {
        musicNumber.text = (int)(volumeLevelMusic*100) + "";
        if (volumeLevelSFX == 1) {
            SFXNumber.text = "ON";
        }
        else
        {
            SFXNumber.text = "OFF";
        }
    }

    public void UpdateVolumeMusic()
    {
        volumeLevelMusic = slideMusic.value;
    }

    public void UpdateVolumeSFX()
    {
        SoundEffectsOverworld.volume = slideSFX.value;
        SoundEffectsBattle.volume = slideSFX.value;
        volumeLevelSFX = slideSFX.value;
    }
}
