using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    private void Start()
    {
        // Comprova si existeix una instància de la classe ConexionDatabase
        if (ConexionDatabase.instance != null)
        {
            ConexionDatabase.instance.ReloadScoreUI();// Si existeix, actualitza la interfície gràfica amb la puntuació
        }
        else
        {
            Debug.LogWarning("ConexionDatabase no est� listo.");
        }
    }
}
