using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SaveNamePoints : MonoBehaviour
{
    public TMP_InputField inputNombre;  // Referencia al InputField
    private int puntuacionFinal;  // Puntuaci�n final del jugador
    public GameObject namePanel;
    string nombreJugador;


    public void GuardarDatos()
    {
        nombreJugador = inputNombre.text;  // Obtener el nombre ingresado
        puntuacionFinal = PointColtroller.instance.getPoints();
        // Guardar nombre y puntuaci�n en PlayerPrefs
        PlayerPrefs.SetString("NombreJugador", nombreJugador);
        PlayerPrefs.SetInt("PuntuacionJugador", puntuacionFinal);
        PlayerPrefs.Save();

        Debug.Log("Guardado: " + nombreJugador + " - " + puntuacionFinal);
        namePanel.SetActive(false);
        PlayerInfoController.Instance.saveData();
        ConexionDatabase.instance.InsertTestData();
    }
    void MostrarDatos()
    {
        string nombre = PlayerPrefs.GetString("NombreJugador", "Desconocido");
        int puntuacion = PlayerPrefs.GetInt("PuntuacionJugador", 0);

        Debug.Log("Jugador: " + nombre + " - Puntuaci�n: " + puntuacion);
    }
    public string getNamePlayer()
    {
        return nombreJugador;
    }
}
