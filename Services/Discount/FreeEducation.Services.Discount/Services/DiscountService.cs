using Dapper;
using FreeEducation.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace FreeEducation.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        public readonly IDbConnection _connection;

        public DiscountService(IDbConnection connection,IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public Task<ResponseDto<NoContent>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("Select * from discount");

            return ResponseDto<List<Models.Discount>>.Success(discounts.ToList(),200);
        }

        public Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoContent>> Save(Models.Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            throw new NotImplementedException();
        }
    }
}
