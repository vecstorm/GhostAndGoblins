using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    public GameObject character;


    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");

        if (character == null)
        {
            Debug.LogError("CamaraController: No se encontró un objeto con la etiqueta 'Player'.");
        }
    }

    void Update()
    {
        if (character != null)
        {
            transform.position = new Vector3(character.transform.position.x, transform.position.y, transform.position.z);
        }
    }


}
