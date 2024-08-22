using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Light things
    Light flashlight;
    
    // Noise things
    public AudioClip flashlight_on_noise;
    public AudioClip flashlight_off_noise;
    AudioSource audio_player;

    void Start()
    {
        flashlight = GetComponent<Light>();
        audio_player = GetComponent<AudioSource>();
    }

    // FLASHLIGHT
    void Update()
    {
        // toggling flashlight if flashlight button pressed
        if (Input.GetButtonDown("Flashlight")) {
            flashlight.enabled = !flashlight.enabled;
            if (flashlight.enabled) {
                TurnOnNoise();
            } else {
                TurnOffNoise();
            }
        }
    }
    
    // AUDIO
    void TurnOnNoise() 
    {
        audio_player.clip = flashlight_on_noise;
        audio_player.Play();
    }
    void TurnOffNoise()
    {
        audio_player.clip = flashlight_off_noise;
        audio_player.Play();
    }
}
