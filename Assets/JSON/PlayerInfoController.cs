using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    [SerializeField] PlayerInfoSerialized playerInfoSerialized;

    [SerializeField]public PlayerInfoSerialized gameData = new PlayerInfoSerialized();

    public static PlayerInfoController Instance { get; private set; }

    private void Awake()
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

    public void saveData()
    {
        gameData.name = "pedro";
        gameData.highScore = PointColtroller.instance.getPoints(); ;
        gameData.livesRemaining = 2;
        SaveGameData.SaveDataInfo(gameData);
    }
}
