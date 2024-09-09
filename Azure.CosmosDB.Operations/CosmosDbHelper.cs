using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CosmosDB.Operations
{
    internal class CosmosDbHelper
    {

        public static void InsertItem(string json) {

            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            // Convert JSON string to BsonDocument
            var document = BsonDocument.Parse(json);

            // Extract the 'id' value from the JSON document dynamically
            var storyId = document["story"]["id"].AsInt32;
            var filter = Builders<BsonDocument>.Filter.Eq("story.id", storyId);
            client.GetDatabase("storyblok")
                .GetCollection<BsonDocument>("storyblokstories").UpdateOne(filter, document, options: new UpdateOptions() { IsUpsert = true });
                //.InsertOne(BsonDocument.Parse(json));

        }
    }
}
