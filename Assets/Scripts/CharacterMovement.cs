using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // constants
    public float movement_speed = 2.5f;
    public float jump_strength = 4.0f;
    public float gravity = 9.81f;
    public float sprint_multiplier = 2.0f;
    public float max_sprint_time = 4.0f;

    // information stored between frames 
    private float sprint_time;
    private Vector3 player_velocity = new Vector3(0.0f, 0.0f, 0.0f); 
    private bool sprinting = false;
    
    // objects used 
    private Rigidbody rb;
    private Transform camera_transform;
    private GameObject groundColliderObject;
    private GroundCollider groundCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera_transform = transform.Find("Main Camera");
        groundColliderObject = GameObject.Find("Ground Collider");
        groundCollider = groundColliderObject.GetComponent<GroundCollider>();   
        sprint_time = max_sprint_time;
    }
    
    void FixedUpdate()
    {
        // **** control **** // 
        bool is_grounded = groundCollider.IsOverlapping();
        float forward_input = Input.GetAxis("Vertical");
        float rightward_input = Input.GetAxis("Horizontal");

        // sprinting changes to matches to match sprint button only if grounded; otherwise retains previous value
        if (is_grounded) {
            sprinting = Input.GetButton("Sprint");
        }

        // if sprinting, sprint timer decreases proportional to total movement
        if (sprinting && sprint_time > 0.0f) {
            sprint_time -= Time.deltaTime * (Mathf.Abs(forward_input) + Mathf.Abs(rightward_input));
        }
        
        // regain sprint when not sprinting OR when not moving
        if (sprint_time < max_sprint_time && (!sprinting || (forward_input == 0 && rightward_input == 0))) {
            sprint_time += Time.deltaTime; 
        }

        // **** translation **** // 
        // getting rotation direction
        Vector3 forward_vector = camera_transform.forward; 
        Vector3 rightward_vector = camera_transform.right;
        
        // removing extra axis and normalizing to match
        forward_vector.y = 0;
        rightward_vector.y = 0;
        forward_vector = Vector3.Normalize(forward_vector);
        rightward_vector = Vector3.Normalize(rightward_vector);

        // constructing movement vector
        forward_vector *= forward_input * movement_speed;
        rightward_vector *= rightward_input * movement_speed;
        
        Vector3 translate = forward_vector + rightward_vector;

        // sprinting multiplier
        if (sprinting && sprint_time > 0) {
            translate *= sprint_multiplier;
        }

        // **** jumping **** //
        if (is_grounded && Input.GetButton("Jump")) {
            player_velocity.y = jump_strength;
        }
        else if (is_grounded && player_velocity.y < 0) {
            player_velocity.y = 0;
        }
        if (!is_grounded) {
            player_velocity.y -= gravity * Time.deltaTime;
        }
        translate.y += player_velocity.y;

        // **** applying movement **** // 
        rb.MovePosition(transform.position + translate * Time.deltaTime);

        // **** playing audio **** //
        groundCollider.DecreaseFootstepTimer(translate);
        if (translate != Vector3.zero && is_grounded) {
            groundCollider.PlayFootstep();
        }
    }

    // UI needs to know amount of sprint time left
    public float GetCurrentSprintTime() {
        return sprint_time;
    }
    public float GetMaxSprintTime() {
        return max_sprint_time;
    }
}