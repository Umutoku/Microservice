using FreeEducation.Services.Catalog.Models;
using FreeEducation.Services.Catalog.Repositories.Interfaces;
using FreeEducation.Services.Catalog.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeEducation.Services.Catalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryCollection.Find(category => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoryCollection.Find<Category>(category => category.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
        }
    }
}
