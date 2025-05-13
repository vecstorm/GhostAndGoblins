using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    private int topScore;
    private TextMeshProUGUI textMeshMaxPoints;
    // Start is called before the first frame update
    void Start()
    {
        textMeshMaxPoints = GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        topScore = PointColtroller.instance.getHighScore();
        textMeshMaxPoints.text = topScore.ToString("0");

    }
}
