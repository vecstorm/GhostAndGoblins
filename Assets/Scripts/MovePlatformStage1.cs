using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformStage1 : MonoBehaviour
{
    public GameObject[] waypoints;

    public float platformSpeed = 2;

    private int waypointsIndex = 0;
    // Update is called once per frame
    void Update()
    {
         MovePlatform();// Crida la funció que gestiona el moviment
    }

    void MovePlatform()
    {
        // Si la plataforma està molt a prop del waypoint actual...
        if (Vector2.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;// Passa al següent waypoint

             // Si ha arribat al final de la llista de waypoints, torna a començar
            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }

        // Mou la plataforma cap al waypoint actual a una velocitat constant
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, platformSpeed * Time.deltaTime);

    }

}
