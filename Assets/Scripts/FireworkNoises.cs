using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkNoises : MonoBehaviour
{
    [SerializeField] private AudioClip[] noises;
    private AudioSource audio_player;
    private float mean_between_noises = 1.0f;
    private float range_between_noises = 0.1f;
    private float timer = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        audio_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f) {
            audio_player.clip = noises[Random.Range(0, noises.Length - 1)];
            audio_player.Play();
            timer = mean_between_noises + Random.Range(-range_between_noises, range_between_noises);
        }
    }
}
