using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointColtroller : MonoBehaviour
{
public static PointColtroller instance;
    [SerializeField] private int cantidadPuntos;

    private void Awake()
    {
        if(instance == null )
        {
            PointColtroller.instance = this;   
            DontDestroyOnLoad( this.gameObject );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void sumarPuntos(int puntos)
    {
        cantidadPuntos += puntos;

    }

    public int getPoints()
    {
        return cantidadPuntos;
    }
}
