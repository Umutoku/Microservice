﻿using FreeEducation.Shared.Dtos;

namespace FreeEducation.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<Models.Discount>>> GetAll();

        Task<ResponseDto<Models.Discount>> GetById(int id);

        Task<ResponseDto<NoContent>> Save(Models.Discount discount);

        Task<ResponseDto<NoContent>> Update(Models.Discount discount);

        Task<ResponseDto<NoContent>> Delete(int id);

        Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
