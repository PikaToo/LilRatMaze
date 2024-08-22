using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    // prefabs to initialize
    public GameObject WalkingBird; 

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(WalkingBird, new Vector3(60.0f, 8.00f, -60.0f), Quaternion.identity);        
        Instantiate(WalkingBird, new Vector3(-60.0f, 8.00f, 60.0f), Quaternion.identity);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
