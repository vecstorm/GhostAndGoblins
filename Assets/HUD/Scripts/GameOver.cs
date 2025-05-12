using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject reiniciar;
    //public Image GameOver;

    private void Start()
    {
        //GameOver.enabled = false;
        reiniciar.gameObject.SetActive(false);
    }


    private void activarGameOver()
    {
        reiniciar.gameObject.SetActive(true);
        
    }
}
