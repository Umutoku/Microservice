using AutoMapper;
using FreeEducation.Services.Catalog.Dtos;
using FreeEducation.Services.Catalog.Models;
using FreeEducation.Services.Catalog.Repositories;
using FreeEducation.Services.Catalog.Repositories.Interfaces;
using FreeEducation.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeEducation.Services.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateAsync(newCategory);
            return ResponseDto<CategoryDto>.Success(categoryDto, 200);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Category not found", 404);
            }
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
