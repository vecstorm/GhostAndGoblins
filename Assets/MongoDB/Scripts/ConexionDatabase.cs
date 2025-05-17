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
        private MongoClient client;// Client MongoDB
        private IMongoDatabase database; // Referència a la base de dades
        private IMongoCollection<BsonDocument> userCollection;// Col·lecció de documents (jugadors)

        public TextMeshProUGUI outputText;// Referència al text UI on es mostra la taula de puntuacions

        private void Awake()
        {
            if (instance == null)
            {
                ConexionDatabase.instance = this;
                // No utilitzem DontDestroyOnLoad
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Mètode que s'executa a l'inici: connecta amb la base de dades i llegeix dades
        async void Start()
        {
            string connectionString = "mongodb+srv://ghost:GhostAndGoblins1@gandg.rncopjg.mongodb.net/?retryWrites=true&w=majority&appName=GandG";

            try
            {
                // Inicialitza el client, base de dades i col·lecció
                client = new MongoClient(connectionString);
                database = client.GetDatabase("GhostAndGoblins");
                userCollection = database.GetCollection<BsonDocument>("game");

                Debug.Log("MongoDB conectado.");

                // Llegeix les dades i mostra-les a la UI
                await ReadDataAsync();
        }
            catch (System.Exception e)
            {
                Debug.LogError("Error de conexi�n MongoDB: " + e.Message);
            }
        }

        // Mètode per llegir dades de puntuació de la base de dades
        public async System.Threading.Tasks.Task ReadDataAsync()
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Empty;
                var result = await userCollection.Find(filter).ToListAsync();

             // Ordena els resultats per puntuació (descendent) i agafa els 10 primers
            var top10 = result.OrderByDescending(doc => doc["highScore"].ToInt32())
                         .Take(10)
                         .ToList();

            string displayText = "HighScore Table:\n";
            
                // Recorre cada document i afegeix la informació al text
                foreach (var document in top10)
                {
                    string name = document["name"].ToString();
                    int highScore = document["highScore"].ToInt32();
                    displayText += $"Name: {name}, High Score: {highScore}, \n";
                }

                // Actualiza el text en la UI
                if (outputText != null)
                {
                    outputText.text = displayText;
                }
                else
                {
                    Debug.LogWarning("outputText no est� asignado.");
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

        // Metode public per forçar la recarrega de la UI (utilitzat cuan es torna al menu)
        public async void ReloadScoreUI()
        {
            if (outputText == null)
            {
                outputText = GameObject.Find("OutputText")?.GetComponent<TextMeshProUGUI>();
                if (outputText == null)
                {
                    Debug.LogWarning("No se encontr� el TextMeshProUGUI llamado 'OutputText'.");
                    return;
                }
            }

            await ReadDataAsync();
        }

        // Metode para inserir dades de prova en la base de dades
        public void InsertTestData(string name, int score, int enemigos)
        {
            // Crea el document amb les dades
            var document = new BsonDocument
        {
            { "name", name },
            { "highScore", score },
            { "enemigos derrotados", enemigos }
        };

            // Inserta el document a la coleccio
            try
            {
                userCollection.InsertOne(document);
                Debug.Log("Documento insertado con �xito.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al insertar datos: " + e.Message);
            }
        }
    }
