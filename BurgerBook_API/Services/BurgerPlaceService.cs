using System;
using BurgerBook.Models.Database;
using BurgerBook_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BurgerBook.Services
{
	public class BurgerPlaceService
	{
        private readonly IMongoCollection<BurgerPlace> _burgerPlaceCollection;

        public BurgerPlaceService(
            IOptions<BurgerBookDatabaseSettings> burgerBookDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                burgerBookDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                burgerBookDatabaseSettings.Value.DatabaseName);

            _burgerPlaceCollection = mongoDatabase.GetCollection<BurgerPlace>(
                burgerBookDatabaseSettings.Value.BurgerPlaceCollectionName);
        }

        public async Task<List<BurgerPlace>> GetAsync() =>
            await _burgerPlaceCollection.Find(_ => true).ToListAsync();

        public async Task<BurgerPlace?> GetAsync(string id) =>
            await _burgerPlaceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BurgerPlace newBurgerPlace) =>
            await _burgerPlaceCollection.InsertOneAsync(newBurgerPlace);

        public async Task UpdateAsync(string id, BurgerPlace updatedBurgerPlace) =>
            await _burgerPlaceCollection.ReplaceOneAsync(x => x.Id == id, updatedBurgerPlace);

        public async Task RemoveAsync(string id) =>
            await _burgerPlaceCollection.DeleteOneAsync(x => x.Id == id);
    }
}

