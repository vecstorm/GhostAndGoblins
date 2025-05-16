using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    private int topScore;
    private TextMeshProUGUI textMeshMaxPoints;

    public static HighScore Instance { get; private set; }   

    public int TopScore => topScore;
    // Start is called before the first frame update

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
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
