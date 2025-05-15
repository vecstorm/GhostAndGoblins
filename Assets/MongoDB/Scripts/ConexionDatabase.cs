using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class ConexionDatabase : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> userCollection;

    async void Start()
    {
        string connectionString = "mongodb + srv://ghost:<db_password>@gandg.rncopjg.mongodb.net/";
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
}
