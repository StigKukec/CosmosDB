using App.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DALModels
{
    public class CosmosDBService : ICosmosDBService
    {
        private readonly Container container;
        public CosmosDBService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task CreatePersonAsync(VMPerson person)
        {
            await container.CreateItemAsync(person, new PartitionKey(person.IDPerson));
        }

        public async Task DeletePersonAsync(VMPerson person)
        {
            await container.DeleteItemAsync<VMPerson>(person.IDPerson, new PartitionKey(person.IDPerson));
        }

        public async Task<IEnumerable<VMPerson>> GetPeopleAsync(string queryString)
        {
            List<VMPerson> persons = new List<VMPerson>();
            var query = container.GetItemQueryIterator<VMPerson>(new QueryDefinition(queryString));
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                persons.AddRange(result.ToList());
            }
            return persons;
        }

        public async Task<VMPerson?> GetPersonAsync(string idPerson)
        {
            try
            {
                return await container.ReadItemAsync<VMPerson>(idPerson, new PartitionKey(idPerson));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdatePersonAsync(VMPerson person)
        {
            await container.UpsertItemAsync<VMPerson>(person, new PartitionKey(person.IDPerson));
        }
    }
}
