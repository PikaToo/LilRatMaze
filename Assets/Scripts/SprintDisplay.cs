using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintDisplay : MonoBehaviour
{
    public float width = 146;
    private GameObject player;
    private CharacterMovement playerMovementScript;  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovementScript = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // getting sprint time left
        float sprint_time = playerMovementScript.GetCurrentSprintTime();
        
        // parsing sprint time to desired relative size 
        float x_transform_scale = Mathf.Max(sprint_time, 0) / playerMovementScript.GetMaxSprintTime();
        
        // setting transform
        transform.localScale = new Vector3(x_transform_scale, 1, 1);
        transform.localPosition = new Vector3(0.5f * width * (x_transform_scale-1), 0, 0);
    }
}
