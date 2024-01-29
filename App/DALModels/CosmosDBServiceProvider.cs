using Microsoft.Azure.Cosmos;

namespace App.DALModels
{
    public static class CosmosDBServiceProvider
    {
        private const string DatabaseName = "Life";
        private const string ContainerName = "Person";
        private const string Account = "https://skukec.documents.azure.com:443/";
        private const string Key = "LwnvxuM8oWxt45zo4uzpJ1GI4ozevexWUASDkFWktsd9xbx4PdnQLJgegzwH0XRZOimoXuCPUbElACDbNBeUqA==";

        private static ICosmosDBService? service;
        public static ICosmosDBService? Service { get => service; }
        public async static Task Init()
        {
            CosmosClient cosmosClient = new(Account,Key);
            service = new CosmosDBService(cosmosClient,DatabaseName, ContainerName);
            DatabaseResponse databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(ContainerName,"/id");
        }
    }
}