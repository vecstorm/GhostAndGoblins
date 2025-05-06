using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    public GameObject waypoint;

    public float platformSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        /*if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.1f)
        {*/
            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, platformSpeed * Time.deltaTime);
       // }

        

    }

}
