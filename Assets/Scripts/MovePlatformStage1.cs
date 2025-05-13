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
         MovePlatform();
    }

    void MovePlatform()
    {
        if (Vector2.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;

            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, platformSpeed * Time.deltaTime);

    }

}
