using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class ConexionDatabase : MonoBehaviour
{
    public static ConexionDatabase instance;
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> userCollection;
    //public SaveGameData gameData = new SaveGameData();
    public PlayerInfoSerialized gameData = new PlayerInfoSerialized();


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
        }
        catch (System.Exception e)
        {
            {
                Debug.LogError("MongoDB Connection Error: " + e.Message);
            }
        }
        
    }
    public void InsertTestData()
    {
        //var collection = database.GetCollection<BsonDocument>("game");

        PlayerInfoController.Instance.saveData();
        var document = new BsonDocument
        {
            { "name", gameData.name },
            { "highScore", gameData.highScore },
            { "livesRemaining", gameData.livesRemaining }
        };

        userCollection.InsertOne(document);
        Debug.Log("Documento insertado.");
    }

}
