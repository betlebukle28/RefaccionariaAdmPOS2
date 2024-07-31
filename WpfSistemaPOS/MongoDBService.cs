using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSistemaPOS
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService()
        {
            var connectionString = "mongodb+srv://axelmendietta28:oq1yMGSpSdm80nYv@posdatabase.yo9hm3d.mongodb.net/?retryWrites=true&w=majority&appName=POSDatabase";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("POSDatabase"); // Nombre de tu base de datos en MongoDB
        }

        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            return _database.GetCollection<BsonDocument>(collectionName);
        }

        public async Task<List<BsonDocument>> GetItemsAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            return documents;
        }

        public async Task InsertItemAsync(string collectionName, BsonDocument document)
        {
            var collection = GetCollection(collectionName);
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateItemAsync(string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            var collection = GetCollection(collectionName);
            await collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteItemAsync(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = GetCollection(collectionName);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<bool> VerifyUserCredentialsAsync(string username, string password)
        {
            var collection = GetCollection("Usuarios"); // Nombre de la colección de usuarios
            var filter = Builders<BsonDocument>.Filter.Eq("usuario", username) & Builders<BsonDocument>.Filter.Eq("password", password);
            var user = await collection.Find(filter).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<BsonDocument> GetItemByIdCompuestoAsync(string idCompuesto)
        {
            var collection = GetCollection("Articulos");
            var filter = Builders<BsonDocument>.Filter.Eq("IdCompuesto", idCompuesto);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<BsonDocument>> GetItemsByDescripcionAsync(string descripcion)
        {
            var collection = GetCollection("Articulos");
            var filter = Builders<BsonDocument>.Filter.Regex("Descripcion", new BsonRegularExpression(descripcion, "i"));
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<List<BsonDocument>> GetItemsByPartialIdOrDescriptionAsync(string text)
        {
            var collection = GetCollection("Articulos");
            var filter = Builders<BsonDocument>.Filter.Regex("IdCompuesto", new BsonRegularExpression(text, "i")) |
                         Builders<BsonDocument>.Filter.Regex("Descripcion", new BsonRegularExpression(text, "i"));
            return await collection.Find(filter).ToListAsync();
        }
    }
}
