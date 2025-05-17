
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
                DontDestroyOnLoad(this.gameObject); // No destrueix l'objecte quan canviem d'escena
                gameData = new PlayerInfoSerialized();// Crea un nou objecte de dades del jugador
            }
        }

        public void saveData()
        {
        gameData.highScore = HighScore.Instance.TopScore;  //Assigna el valor actual del TopScore a l'objecte gameData
        SaveGameData.SaveDataInfo(gameData);// Desa les dades al disc utilitzant la classe SaveGameData
        }
    }
