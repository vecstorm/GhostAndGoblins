using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    [SerializeField]
    public GameObject namePanel;
    public static ScoreTable instance;
    ConexionDatabase conexionDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            ScoreTable.instance = this;
            DontDestroyOnLoad(this.gameObject);
            ActivarTablaPuntuaciones();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    async void Start()
    {
        // Llamar a ReadDataAsync directamente con await
        await conexionDatabase.ReadDataAsync();
        
    }


    public void ActivarTablaPuntuaciones()
    {
        namePanel.SetActive(true);
    }

    public void DesactivarTablaPuntuaciones()
    {
        namePanel.SetActive(false);
    }

}
