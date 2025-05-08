using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControler : MonoBehaviour
{
    public static PointControler Instance;

    [SerializeField] private int totalPoints;

    private void Awake()
    {
        if (PointControler.Instance == null)
        {
            PointControler.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void sumarPuntos(int puntos)
            {
                totalPoints += puntos;
                Debug.Log(totalPoints);
            }
}
