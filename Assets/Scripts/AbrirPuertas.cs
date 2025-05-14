using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirPuertas : MonoBehaviour
{
    public GameObject waypoint;


    public float platformSpeed = 2;

    // Update is called once per frame

    private void Start()
    {
        this.enabled = false; 


    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
            
        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, platformSpeed * Time.deltaTime);

    }

}
