using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAmbienceScript : MonoBehaviour
{
    private float time_between_plays = 60.0f;
    private float play_timer = 0.0f;
    AudioSource audio_player;
    void Start()
    {
        audio_player = GetComponent<AudioSource>();
        WalkingBirdAI.GetAmbienceScript(this.gameObject);
    }

    void Update()
    {
        if (play_timer > 0.0f) {
            play_timer -= Time.deltaTime;
        }
    }

    public void PlayAmbience(float distance_from_monster, float audio_threshold)
    {
        if (distance_from_monster < audio_threshold && play_timer <= 0.0f) {
            audio_player.Play();
            audio_player.volume = Mathf.Clamp((audio_threshold - distance_from_monster) / audio_threshold, 0.1f, 0.6f);
            play_timer = time_between_plays;
        }
    }
}
