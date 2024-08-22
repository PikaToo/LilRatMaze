using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheeseScript : MonoBehaviour
{
    public float x_rotate_speed = 0.2f;
    public float y_rotate_speed = 1.0f;
    public float z_rotate_speed = 1.0f;
    public float x_rotate_amplitude = 0.1f;
    public float y_rotate_amplitude = 0.1f;
    public float z_rotate_amplitude = 0.1f;

    void Update()
    {
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene(2);
        }
    }

    void Rotate()
    {
        float x_rotate = Mathf.Sin(Time.time * x_rotate_speed);
        float y_rotate = Mathf.Sin(Time.time * y_rotate_speed);
        float z_rotate = Mathf.Sin(Time.time * z_rotate_speed);
        transform.Rotate(new Vector3(x_rotate_amplitude * x_rotate, y_rotate_amplitude * y_rotate, z_rotate_amplitude * z_rotate));
    }
}
