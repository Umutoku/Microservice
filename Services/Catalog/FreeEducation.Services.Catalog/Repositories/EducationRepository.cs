using FreeEducation.Services.Catalog.Models;
using FreeEducation.Services.Catalog.Settings;
using MongoDB.Driver;

namespace FreeEducation.Services.Catalog.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly IMongoCollection<Education> _educationCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public EducationRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _educationCollection = database.GetCollection<Education>(databaseSettings.EducationCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<List<Education>> GetAllAsync()
        {
            var educations = await _educationCollection.Find(category => true).ToListAsync();

            if(educations.Count != 0)
            {
                foreach (var education in educations)
                {
                    education.Category = await _categoryCollection.Find<Category>(x => x.Id == education.CategoryId).FirstAsync();
                }
            }   
            else
            {
                educations = new List<Education>();
            }
            return educations;
        }

        public async Task<Education> GetByIdAsync(string id)
        {
            return await _educationCollection.Find<Education>(education => education.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Education>> GetAllByUserIdAsync(string userId)
        {
            var educations = await _educationCollection.Find<Education>(x=>x.UserId== userId).ToListAsync();

            if (educations.Count != 0)
            {
                foreach (var education in educations)
                {
                    education.Category = await _categoryCollection.Find<Category>(x => x.Id == education.CategoryId).FirstAsync();
                }
            }
            else
            {
                educations = new List<Education>();
            }
            return educations;
        }

        public async Task CreateAsync(Education education)
        {
            await _educationCollection.InsertOneAsync(education);
        }

        public async Task UpdateAsync(Education education)
        {
             await _educationCollection.FindOneAndReplaceAsync(x => x.Id == education.Id, education);
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            return await _educationCollection.DeleteOneAsync(x => x.Id == id);
        }
    }


}
