using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public TextMeshProUGUI outputText;

        private void Awake()
        {
            if (instance == null)
            {
                ConexionDatabase.instance = this;
                // No usaremos DontDestroyOnLoad
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Método para conectar a la base de datos
        async void Start()
        {
            string connectionString = "mongodb+srv://ghost:GhostAndGoblins1@gandg.rncopjg.mongodb.net/?retryWrites=true&w=majority&appName=GandG";

            try
            {
                client = new MongoClient(connectionString);
                database = client.GetDatabase("GhostAndGoblins");
                userCollection = database.GetCollection<BsonDocument>("game");

                Debug.Log("MongoDB conectado.");

                await ReadDataAsync();
        }
            catch (System.Exception e)
            {
                Debug.LogError("Error de conexión MongoDB: " + e.Message);
            }
        }

        // Método para leer los datos de la base de datos
        public async System.Threading.Tasks.Task ReadDataAsync()
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = await userCollection.Find(filter).ToListAsync();

            var top10 = result.OrderByDescending(doc => doc["highScore"].ToInt32())
                         .Take(10)
                         .ToList();

            string displayText = "HighScore Table:\n";
                foreach (var document in top10)
                {
                    string name = document["name"].ToString();
                    int highScore = document["highScore"].ToInt32();
                    displayText += $"Name: {name}, High Score: {highScore}, \n";
                }

                // Actualiza el texto en la UI
                if (outputText != null)
                {
                    outputText.text = displayText;
                }
                else
                {
                    Debug.LogWarning("outputText no está asignado.");
                }
            }
            catch (System.Exception e)
            {
                if (outputText != null)
                {
                    outputText.text = "Error al leer los datos: " + e.Message;
                }
            }
        }

        // Método público para forzar la recarga de la UI (usado cuando se vuelve al menú)
        public async void ReloadScoreUI()
        {
            if (outputText == null)
            {
                outputText = GameObject.Find("OutputText")?.GetComponent<TextMeshProUGUI>();
                if (outputText == null)
                {
                    Debug.LogWarning("No se encontró el TextMeshProUGUI llamado 'OutputText'.");
                    return;
                }
            }

            await ReadDataAsync();
        }

        // Método para insertar datos de prueba en la base de datos
        public void InsertTestData(string name, int score, int enemigos)
        {
            // Crea el documento con los datos
            var document = new BsonDocument
        {
            { "name", name },
            { "highScore", score },
            { "enemigos derrotados", enemigos }
        };

            // Inserta el documento en la colección
            try
            {
                userCollection.InsertOne(document);
                Debug.Log("Documento insertado con éxito.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al insertar datos: " + e.Message);
            }
        }
    }
