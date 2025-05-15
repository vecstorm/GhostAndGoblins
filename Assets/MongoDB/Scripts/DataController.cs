using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{

    public string archivoInfoPartida;
    public GuardadoDatos datosJuego;
    SaveNamePoints nombre;

    private void Awake()
    {
        archivoInfoPartida = Application.dataPath + "/GuardadoDatos.json";
    }


    private void CargarDatos()
    {
        if (File.Exists(archivoInfoPartida))
        {
            string contenido = File.ReadAllText(archivoInfoPartida);
            datosJuego = JsonUtility.FromJson<GuardadoDatos>(contenido);
        }
        else
        {
            Debug.Log("EL arcghivo no existe");
        }
    }

    public void GuardarDatos()
    {
        GuardadoDatos nuevosDatos = new();

        nuevosDatos.nombreJugador = nombre.getNamePlayer();
        nuevosDatos.puntos = PointColtroller.instance.getPoints();
        

        string cadenaJson = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoInfoPartida, cadenaJson);

        Debug.Log("archivo creado");
    }
}
