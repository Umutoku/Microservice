using FreeEducation.Services.Catalog.Models;
using MongoDB.Driver;

namespace FreeEducation.Services.Catalog.Repositories
{
    public interface IEducationRepository
    {
        Task<List<Education>> GetAllAsync();
        Task<Education> GetByIdAsync(string id);
        Task CreateAsync(Education education);
        Task<List<Education>> GetAllByUserIdAsync(string userId);
        Task UpdateAsync(Education education);
        Task<DeleteResult> DeleteAsync(string id);
    }
}
