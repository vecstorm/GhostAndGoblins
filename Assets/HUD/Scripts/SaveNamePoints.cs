using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SaveNamePoints : MonoBehaviour
{
    public TMP_InputField inputNombre;  // Referencia al InputField
    private int puntuacionFinal, shoots, saltos, enemigos;  // Puntuaci�n final del jugador
    public GameObject namePanel;
    string nombreJugador;
    CharacterAbilityController stats;
    MovimientoJugador estadisticas;



    public async void GuardarDatos()
    {
        nombreJugador = inputNombre.text;  // Obtener el nombre ingresado
        puntuacionFinal = PointColtroller.instance.getPoints();
        enemigos = PointColtroller.instance.GetEnemigosMuertos();
        //shoots = stats.GetCantidadDisparos();
        //saltos = estadisticas.TotalSaltos();
        // Guardar nombre y puntuaci�n en PlayerPrefs
        PlayerPrefs.SetString("NombreJugador", nombreJugador);
        PlayerPrefs.SetInt("PuntuacionJugador", puntuacionFinal);
        PlayerPrefs.Save();

        Debug.Log("Guardado: " + nombreJugador + " - " + puntuacionFinal);
        namePanel.SetActive(false);
        PlayerInfoController.Instance.saveData();
        
        ConexionDatabase.instance.InsertTestData(nombreJugador, puntuacionFinal, enemigos);
        await ConexionDatabase.instance.ReadDataAsync();
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
