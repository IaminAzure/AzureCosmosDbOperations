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

        internal static async Task UpsertStories(string json) {

            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            // Convert JSON string to BsonDocument
            var document = BsonDocument.Parse(json);

            // Extract the 'id' value from the JSON document dynamically
            var storyId = document["story"]["id"].AsInt32;
            var filter = Builders<BsonDocument>.Filter.Eq("story.id", storyId);
            var data= client.GetDatabase("storyblok")
                .GetCollection<BsonDocument>("storyblokstories").Find(filter).ToList(); //.UpdateOne(filter, document, options: new UpdateOptions() { IsUpsert = true });
                                                                                        //.InsertOne(BsonDocument.Parse(json));
           await client.GetDatabase("storyblok")
           .GetCollection<BsonDocument>("storyblokstories").ReplaceOneAsync(filter, document,new ReplaceOptions() { IsUpsert = true});
        }

        internal static async Task UpsertAssets(string requestBody)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            // Convert JSON string to BsonDocument
            var document = BsonDocument.Parse(requestBody);

            // Extract the 'id' value from the JSON document dynamically
            var assetId = document["asset"]["id"].AsInt32;
            var filter = Builders<BsonDocument>.Filter.Eq("asset.id", assetId);        
           await client.GetDatabase("storyblok")
            .GetCollection<BsonDocument>("storyblokassets").ReplaceOneAsync(filter, document, new ReplaceOptions() { IsUpsert = true });
        }


        internal static async Task DeleteStory(int storyId)
        {

            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            var filter = Builders<BsonDocument>.Filter.Eq("story.id", storyId);
            var data = client.GetDatabase("storyblok")
                .GetCollection<BsonDocument>("storyblokstories").Find(filter).ToList();

            if (data.Count > 0)
               await client.GetDatabase("storyblok")
                .GetCollection<BsonDocument>("storyblokstories").DeleteOneAsync(filter);
        }

        internal static async Task DeleteAsset(int assetId)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));
            var filter = Builders<BsonDocument>.Filter.Eq("asset.id", assetId);
            var data = client.GetDatabase("storyblok")
             .GetCollection<BsonDocument>("storyblokassets").Find(filter).ToList();

            if (data.Count > 0)
             await   client.GetDatabase("storyblok")
            .GetCollection<BsonDocument>("storyblokassets").DeleteOneAsync(filter);
        }
    }
}
