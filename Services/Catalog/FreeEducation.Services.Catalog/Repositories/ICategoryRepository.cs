using FreeEducation.Services.Catalog.Models;

namespace FreeEducation.Services.Catalog.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task CreateAsync(Category category);
        Task<Category> GetByIdAsync(string id);
    }
}
