using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WalkingBirdAI : MonoBehaviour
{
    // movement variables
    public float rotation_speed_multiplier = 3.0f;
    // public float distance_to_player_before_stopping = 3.0f;
    public float target_angle_to_player = 2.0f;
    
    private float detection_range = 100.0f;
    private float ambience_threshold = 100.0f;
    private float roar_threshold = 50.0f;
    
    private GameObject player_object;
    private Transform player_transform;
    private NavMeshAgent agent;


    // audio - roar variables
    public float roar_time_delay = 40.0f;
    private float roar_timer = 0.0f;
    AudioSource audio_player;

    // audio - ambience 
    // assigning monster ambience script
    public static MonsterAmbienceScript ambience_script;
    public static void GetAmbienceScript(GameObject m) {
        ambience_script = m.GetComponent<MonsterAmbienceScript>();
    }
    void Start()
    {
        player_object = GameObject.Find("Player");
        player_transform = player_object.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        audio_player = GetComponent<AudioSource>();
    }

    void Update()
    {
        // TRANSLATION TO PLAYER //
        // find distance from player
        Vector3 difference = player_transform.position - transform.position;
        difference.y = 0;
        
        // if within detection range, try to go to player
        if (difference.magnitude < detection_range) {
            agent.isStopped = false;
            agent.destination = player_transform.position;
 
        } else {
            agent.isStopped = true;
        }

        // AUDIO //
        // if close to player and roar time done, play sound
        if (difference.magnitude < roar_threshold && roar_timer <= 0.0f) {
            audio_player.Play();
            roar_timer = roar_time_delay;
        }

        ambience_script.PlayAmbience(difference.magnitude, ambience_threshold);
        
        if (roar_timer > 0.0f) {
            roar_timer -= Time.deltaTime;
        }
        
        // ROTATION //
        // find current velocity
        Vector3 velocity = agent.velocity;
        velocity.y = 0;
        
        // if moving, face movement direction
        if (velocity != Vector3.zero) {
            
            Quaternion rotation_direction = Quaternion.LookRotation(velocity);
            rotation_direction *= Quaternion.Euler(Vector3.up * -90);
            transform.rotation = rotation_direction;
        }

        // if stationary, face player
        else {
            // finding vector to face to placer
            Vector3 target_direction = player_transform.position - transform.position; 
            
            // only rotate if angle is large
            float angle = Vector3.Angle(target_direction, transform.forward);
            if (Mathf.Abs(angle - 90.0f) > target_angle_to_player) {
                
                // rotating by 90 to account for monster's rotation being off 
                target_direction = Quaternion.Euler(0,-90,0) * target_direction; 

                float rotation_speed = rotation_speed_multiplier * Time.deltaTime;

                Vector3 new_direction = Vector3.RotateTowards(transform.forward, target_direction, rotation_speed, 0.0f); 

                transform.rotation = Quaternion.LookRotation(new_direction);
            }

        }

    }

    // death
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene(3);
        }
    }
}
