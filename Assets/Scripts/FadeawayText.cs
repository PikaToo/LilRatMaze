using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeawayText : MonoBehaviour
{
    private float time_until_start_fading = 8.0f;
    private float fade_speed = 1.0f;
    private Text text;
    private Color end_color = new Color(0, 0, 0, 0);
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_until_start_fading > 0.0f) {
            time_until_start_fading -= Time.deltaTime;
        } else {
            text.color = Color.Lerp(text.color, end_color, fade_speed * Time.deltaTime);
        }
    }
}
