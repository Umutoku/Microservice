using FreeEducation.Services.Basket.Dtos;
using FreeEducation.Shared.Dtos;
using System.Text.Json;

namespace FreeEducation.Services.Basket.Services
{
    public class BasketService(RedisService redisService) : IBasketService
    {
        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var status = await redisService.GetDb().KeyDeleteAsync(userId);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket not found", 404);

        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await redisService.GetDb().StringGetAsync(userId);

            if(String.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket not found", 404);
            }

            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);


        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could not update or save", 500);
        }
    }
}
