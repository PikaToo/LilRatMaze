using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public float footstep_frequency_multiplier = 1.0f;
    public AudioClip[] footstep_sounds;

    AudioSource audio_player;
    float time_since_last_footstep = 0.0f;

    int collider_overlaps;
    
    // GROUND COLLISION    
    public bool IsOverlapping() {
        return collider_overlaps > 1;    // needs one more than 1, as 1 is the default because colliding with player
    }
 
    void OnTriggerEnter(Collider other) {
        collider_overlaps++;
    }

    void OnTriggerExit(Collider other) {
        collider_overlaps--;
    }

    // AUDIO
    void Start() {
        audio_player = GetComponent<AudioSource>();
    }
    
    public void DecreaseFootstepTimer(Vector3 velocity) {
        velocity.y = 0.0f;
        time_since_last_footstep += Time.deltaTime * velocity.magnitude * footstep_frequency_multiplier;
    }

    public void PlayFootstep() {
        if (time_since_last_footstep > 2.0f) {
            audio_player.clip = footstep_sounds[Random.Range(0, footstep_sounds.Length - 1)];
            audio_player.Play();
            time_since_last_footstep = 0.0f;
        }
    }

}
