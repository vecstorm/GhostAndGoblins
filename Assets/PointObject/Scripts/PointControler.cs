using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointControler : MonoBehaviour
{
    public static PointControler Instance;
    [SerializeField] private int totalPoints;

    private TextMeshProUGUI textMeshCurrentPoints;

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
    // Start is called before the first frame update
    void Start()
    {
        textMeshCurrentPoints = GetComponent<TextMeshProUGUI>();

    }

    public void sumarPuntos(int puntos)
    {
        totalPoints += puntos;
        Debug.Log(totalPoints);
    }



    // Update is called once per frame
    void Update()
    {
        textMeshCurrentPoints.text = totalPoints.ToString("0");


    }


}
