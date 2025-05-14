using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SaveNamePoints : MonoBehaviour
{
    public TMP_InputField inputNombre;  // Referencia al InputField
    private int puntuacionFinal;  // Puntuación final del jugador
    public GameObject namePanel;


    public void GuardarDatos()
    {
        string nombreJugador = inputNombre.text;  // Obtener el nombre ingresado
        puntuacionFinal = PointColtroller.instance.getPoints();
        // Guardar nombre y puntuación en PlayerPrefs
        PlayerPrefs.SetString("NombreJugador", nombreJugador);
        PlayerPrefs.SetInt("PuntuacionJugador", puntuacionFinal);
        PlayerPrefs.Save();

        Debug.Log("Guardado: " + nombreJugador + " - " + puntuacionFinal);
        namePanel.SetActive(false);
    }
    void MostrarDatos()
    {
        string nombre = PlayerPrefs.GetString("NombreJugador", "Desconocido");
        int puntuacion = PlayerPrefs.GetInt("PuntuacionJugador", 0);

        Debug.Log("Jugador: " + nombre + " - Puntuación: " + puntuacion);
    }
}
