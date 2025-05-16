using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    private void Start()
    {
        if (ConexionDatabase.instance != null)
        {
            ConexionDatabase.instance.ReloadScoreUI();
        }
        else
        {
            Debug.LogWarning("ConexionDatabase no está listo.");
        }
    }
}
