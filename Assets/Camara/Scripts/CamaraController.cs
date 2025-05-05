using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    public Transform character;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.position.x, transform.position.y, transform.position.z);
    }

    
}
