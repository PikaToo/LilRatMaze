using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensitivity = 1.5f;
    private float x_rotate;
    private float y_rotate;
    private Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    { 
        // finding rotation amount
        x_rotate = Input.GetAxis("Mouse Y");
        y_rotate = Input.GetAxis("Mouse X");

        // making vector
        rotate = transform.eulerAngles + new Vector3(-x_rotate * sensitivity, y_rotate * sensitivity, 0);

        // applying vector
        transform.eulerAngles = rotate;
        
    }
}
