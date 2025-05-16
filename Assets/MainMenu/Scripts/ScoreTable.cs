using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] public GameObject namePanel;
    [SerializeField] public TextMeshProUGUI outputText;
    private IMongoCollection<BsonDocument> userCollection;

    public static ScoreTable instance;

    private void Awake()
    {

        // Verificar que outputText está asignado
        if (outputText == null)
        {
            outputText = GameObject.Find("OutputText")?.GetComponent<TextMeshProUGUI>();
            if (outputText == null)
            {
                Debug.LogError("No se encontró un TextMeshProUGUI en la escena.");
            }
        }
    }
    private void Start()
    {
        ActivarTablaPuntuaciones();
    }
    public void ShowNamePanel()
    {
        if (namePanel != null)
        {
            namePanel.SetActive(true);
        }
    }


    public async void ActivarTablaPuntuaciones()
    {
        namePanel.SetActive(true);
        await ReadDataAsync();
    }

    public async Task<string> ReadDataAsync()
    {
        try
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = await userCollection.Find(filter).ToListAsync();

            string displayText = "HighScore Table:\n";

            foreach (var document in result)
            {
                string name = document["name"].ToString();
                int highScore = document["highScore"].ToInt32();
                displayText += $"Name: {name}, High Score: {highScore}, \n";
            }

            return displayText;  // ✅ Ahora devolvemos un string con la información
        }
        catch (System.Exception e)
        {
            return "Error al leer los datos: " + e.Message;
        }
    }

    public void DesactivarTablaPuntuaciones()
    {
        namePanel.SetActive(false);
    }
}
