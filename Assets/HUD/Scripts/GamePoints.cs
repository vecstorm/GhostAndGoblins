using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePoints : MonoBehaviour
{

    private int totalPoints;
    PointObjects pointObject;

    private TextMeshProUGUI textMeshPoints;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPoints = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        totalPoints = PointColtroller.instance.getPoints();
        textMeshPoints.text = totalPoints.ToString("0");

    }



}
