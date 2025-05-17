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
    [SerializeField] public GameObject namePanel;// Panell on es demana el nom del jugador
    [SerializeField] public TextMeshProUGUI outputText;// Text on es mostrarà la taula de puntuacions
    private IMongoCollection<BsonDocument> userCollection;// Col·lecció de MongoDB on s'emmagatzemen les dades dels usuaris

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
        ActivarTablaPuntuaciones();// Quan es carrega el script, s’activa la taula de puntuacions
    }

     // Mostra el panell on el jugador pot introduir el seu nom
    public void ShowNamePanel()
    {
        if (namePanel != null)
        {
            namePanel.SetActive(true);
        }
    }

    // Activa la taula de puntuacions i crida la lectura de dades de la base de dades
    public async void ActivarTablaPuntuaciones()
    {
        namePanel.SetActive(true);
        await ReadDataAsync();
    }

    // Llegeix les dades de puntuacions de la base de dades MongoDB
    public async Task<string> ReadDataAsync()
    {
        try
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = await userCollection.Find(filter).ToListAsync();

            string displayText = "HighScore Table:\n";

            // Recorre cada document obtingut i extreu el nom i la puntuació
            foreach (var document in result)
            {
                string name = document["name"].ToString();
                int highScore = document["highScore"].ToInt32();
                displayText += $"Name: {name}, High Score: {highScore}, \n";
            }

            return displayText;  // Retorna el text a mostrar com a string
        }
        catch (System.Exception e)
        {
            return "Error al leer los datos: " + e.Message;
        }
    }

    // Desactiva el panell de la taula de puntuacions
    public void DesactivarTablaPuntuaciones()
    {
        namePanel.SetActive(false);
    }
}
