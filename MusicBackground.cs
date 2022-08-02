using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicBackground : MonoBehaviour
{
    public AudioClip[] music;

    public AudioSource currMusic;
    private AudioClip newTrack;
    private void Start()
    {
        // Keep the same Music manager prefab.
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<MusicBackground>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currMusic.volume = VolumeManager.volumeLevelMusic;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
            case "Start Scene":
            case "Bar":
            case "Outside":
            case "Store_Outside":
            case "Mansion_Outside":
                newTrack = music[0];
                break;
            case "Store":
                newTrack = music[1];
                break;
            case "Beach":
                newTrack = music[2];
                break;
            case "Mansion_Upstairs":
            case "Mansion_Downstairs":
                newTrack = music[3];
                break;
            case "Battle":
                newTrack = music[4];
                break;
            case "End":
            case "End Scene":
                newTrack = music[5];
                break;
        }

        if ((currMusic.clip == null || currMusic.clip.name != newTrack.name))
        {
            currMusic.Stop();
            currMusic.clip = newTrack;
            currMusic.Play();
        }
    }
}