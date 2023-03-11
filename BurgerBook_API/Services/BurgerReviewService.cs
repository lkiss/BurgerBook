using System;
using BurgerBook.Models.Database;
using BurgerBook_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BurgerBook.Services
{
	public class BurgerReviewService
	{
        private readonly IMongoCollection<BurgerReview> _burgerReviewCollection;

        public BurgerReviewService(
            IOptions<BurgerBookDatabaseSettings> burgerBookDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                burgerBookDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                burgerBookDatabaseSettings.Value.DatabaseName);

            _burgerReviewCollection = mongoDatabase.GetCollection<BurgerReview>(
                burgerBookDatabaseSettings.Value.BurgerReviewCollectionName);
        }

        public async Task<List<BurgerReview>> GetAsync() =>
            await _burgerReviewCollection.Find(_ => true).ToListAsync();

        public async Task<BurgerReview?> GetAsync(string id) =>
            await _burgerReviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        
        public async Task<List<BurgerReview>> GetByBurgerPlaceIdAsync(string burgerPlaceId) =>
            await _burgerReviewCollection.Find(x => x.BurgerPlaceId == burgerPlaceId).ToListAsync();

        public async Task CreateAsync(BurgerReview newBurgerReview) =>
            await _burgerReviewCollection.InsertOneAsync(newBurgerReview);

        public async Task UpdateAsync(string id, BurgerReview updatedBurgerReview) =>
            await _burgerReviewCollection.ReplaceOneAsync(x => x.Id == id, updatedBurgerReview);

        public async Task RemoveAsync(string id) =>
            await _burgerReviewCollection.DeleteOneAsync(x => x.Id == id);

        public async Task RemoveByBurgerPlaceIdAsync(string burgerPlaceId)
        {
            var filterDefinition = Builders<BurgerReview>.Filter.Eq(x => x.BurgerPlaceId, burgerPlaceId);
            await _burgerReviewCollection.DeleteManyAsync(filterDefinition);
        }
    }
}

