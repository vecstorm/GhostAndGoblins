    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor.PackageManager;
    using UnityEngine;

    public class PlayerInfoController : MonoBehaviour
    {
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
                gameData = new PlayerInfoSerialized();
            }
        }

        public void saveData()
        {
        gameData.highScore = HighScore.Instance.TopScore;  // <- ahora accede al valor actual
        SaveGameData.SaveDataInfo(gameData);
        }
    }
