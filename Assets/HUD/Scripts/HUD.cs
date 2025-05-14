using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class HUD : MonoBehaviour
{

    private static HUD instance;  // Instancia estática para evitar duplicados

    void Awake()
    {
        // Si no existe una instancia, la creamos
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Evita que el objeto sea destruido al cambiar de escena
        }
        else
        {
            Destroy(gameObject);  // Si ya existe una instancia, destruimos esta
        }
    }


}

