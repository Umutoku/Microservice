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

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("Delete from discount where id=@id",new {id});

            if(status == 0)
            {
                return ResponseDto<NoContent>.Fail("Discount not found",404);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("Select * from discount");

            return ResponseDto<List<Models.Discount>>.Success(discounts.ToList(),200);
        }

        public async Task<ResponseDto<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _connection.QueryFirstOrDefaultAsync<Models.Discount>("Select * from discount where code=@code and userId=@userId",new {code,userId});

            if(discounts == null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount not found",404);
            }

            return ResponseDto<Models.Discount>.Success(discounts,200);
        }

        public async Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            var discount = await _connection.QueryFirstOrDefaultAsync<Models.Discount>("Select * from discount where id=@id",new {id});
            if(discount == null)
            {
                return ResponseDto<Models.Discount>.Fail("Discount not found",404);
            }
            return ResponseDto<Models.Discount>.Success(discount,200);
        }

        public async Task<ResponseDto<NoContent>> Save(Models.Discount discount)
        {
            var status = await _connection.ExecuteAsync("Insert into discount (code,rate,userId) values (@code,@rate,@userId)",discount);

            if(status == 0)
            {
                return ResponseDto<NoContent>.Fail("An error occured while adding discount",500);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> Update(Models.Discount discount)
        {
            var status = await _connection.ExecuteAsync("Update discount set code=@code,rate=@rate,userId=@userId where id=@id",discount);

            if(status == 0)
            {
                return ResponseDto<NoContent>.Fail("Discount not found",404);
            }

            return ResponseDto<NoContent>.Success(204);
        }

    }
}
