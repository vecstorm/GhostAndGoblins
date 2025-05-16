using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using TMPro;
using UnityEngine;

public class ConexionDatabase : MonoBehaviour
{
    public static ConexionDatabase instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> userCollection;
    //public SaveGameData gameData = new SaveGameData();
    public PlayerInfoSerialized gameData = new PlayerInfoSerialized();

    public TextMeshProUGUI outputText;


    private void Awake()
    {
        if (instance == null)
        {
            ConexionDatabase.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    async void Start()
    {

        string connectionString = "mongodb+srv://ghost:GhostAndGoblins1@gandg.rncopjg.mongodb.net/?retryWrites=true&w=majority&appName=GandG";

        try
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("GhostAndGoblins");
            userCollection = database.GetCollection<BsonDocument>("game");

            await ReadDataAsync();
        }
        catch (System.Exception e)
        {
            {
                Debug.LogError("MongoDB Connection Error: " + e.Message);
            }
        }

    }
    public void InsertTestData(string name, int score, int enemigos)
    {

        PlayerInfoController.Instance.saveData();
        var document = new BsonDocument
        {
            { "name", name },
            { "highScore", score },
            { "enemigos derrotados", enemigos }
            //{ "cantidad disparos", shoots },
            //{ "saltos", saltos }
        };


        userCollection.InsertOne(document);
        Debug.Log("Documento insertado.");
    }
    

    // Función para leer datos de la base de datos
    public async System.Threading.Tasks.Task ReadDataAsync()
    {
        try
        {
            // Realizar la consulta (ejemplo: obtener todos los documentos)
            var filter = Builders<BsonDocument>.Filter.Empty; // Si quieres todos los documentos
            var result = await userCollection.Find(filter).ToListAsync();

            if (result.Count > 0)
            {
                // Crear un texto que concatenará todos los resultados
                string displayText = "HighScore Table:\n";

                // Procesar cada documento (por ejemplo, mostrar la información en consola)
                foreach (var document in result)
                {
                    string name = document["name"].ToString();
                    int highScore = document["highScore"].ToInt32();
                    //int enemigos = document["enemigos derrotados"].ToInt32();

                    // Agregar cada resultado al string
                    displayText += $"Name: {name}, High Score: {highScore}, \n";
                }
                //, Enemigos: {enemigos}

                // Actualizar el texto en la UI
                outputText.text = displayText;
            }
            else
            {
                outputText.text = "No se encontraron datos.";
            }
        }
        catch (System.Exception e)
        {
            outputText.text = "Error al leer los datos: " + e.Message;
            Debug.LogError("Error al leer los datos: " + e.Message);
        }


    }
}
