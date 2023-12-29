using AutoMapper;
using FreeEducation.Services.Catalog.Dtos;
using FreeEducation.Services.Catalog.Models;
using FreeEducation.Services.Catalog.Repositories;
using FreeEducation.Shared.Dtos;
using System.Collections.Generic;
using FreeEducation.Shared.Messages;
using MassTransit;

namespace FreeEducation.Services.Catalog.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public EducationService(IEducationRepository educationRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ResponseDto<List<EducationDto>>> GetAllAsync()
        {
            var educations = await _educationRepository.GetAllAsync();
            return ResponseDto<List<EducationDto>>.Success(_mapper.Map<List<EducationDto>>(educations), 200);
        }

        public async Task<ResponseDto<EducationDto>> CreateAsync(EducationCreateDto educationDto)
        {
            var newEducation = _mapper.Map<Education>(educationDto);
            newEducation.CreatedTime = DateTime.Now;
            await _educationRepository.CreateAsync(newEducation);
            return ResponseDto<EducationDto>.Success(_mapper.Map<EducationDto>(newEducation), 200);
        }

        public async Task<ResponseDto<EducationDto>> GetByIdAsync(string id)
        {
            var education = await _educationRepository.GetByIdAsync(id);
            if (education == null)
            {
                return ResponseDto<EducationDto>.Fail("Category not found", 404);
            }
            return ResponseDto<EducationDto>.Success(_mapper.Map<EducationDto>(education), 200);
        }

        public async Task<ResponseDto<List<EducationDto>>> GetAllByUserIdAsync(string userId)
        {
            var education = await _educationRepository.GetAllByUserIdAsync(userId);
            if (education == null)
            {
                return ResponseDto<List<EducationDto>>.Fail("Category not found", 404);
            }
            return ResponseDto<List<EducationDto>>.Success(_mapper.Map<List<EducationDto>>(education), 200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(EducationUpdateDto educationUpdateDto)
        {
            var newEducation = _mapper.Map<Education>(educationUpdateDto);

            var education = await _educationRepository.GetByIdAsync(newEducation.Id);
            if (education == null)
            {
                return ResponseDto<NoContent>.Fail("Category not found", 404);
            }
            await _educationRepository.UpdateAsync(newEducation);
            await _publishEndpoint.Publish<EducationNameChangedEvent>(new EducationNameChangedEvent()
            {
                EducationId = educationUpdateDto.Id,
                UpdatedName = educationUpdateDto.Name
            }
            );
            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _educationRepository.DeleteAsync(id);
            if(result.DeletedCount > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Education not found", 404);
            }
        }
    }
}
