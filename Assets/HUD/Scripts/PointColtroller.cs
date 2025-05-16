using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointColtroller : MonoBehaviour
{
public static PointColtroller instance;
    [SerializeField] private int cantidadPuntos;
    private int highScore;
    
    private int highScorePartida;

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
    private void Start()
{
    if (PlayerInfoController.Instance?.gameData != null)
    {
        highScore = PlayerInfoController.Instance.gameData.highScore;
    }
    else
    {
        Debug.LogWarning("PlayerInfoController o gameData es null");
    }
}



    public void sumarPuntos(int puntos)
    {
        cantidadPuntos += puntos;
        actualizarHighScore();

    }

    public void actualizarHighScore()
    {
        
        if (highScore < cantidadPuntos) 
        {
            highScore = cantidadPuntos;
        }

    }


    public int getPoints()
    {
        return cantidadPuntos;
    }
    public int getHighScore()
    {
        return highScore;
    }


    public void reiniciarPuntos()
    {
        cantidadPuntos = 0;
    }
}
