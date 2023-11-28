using FreeEducation.Services.Order.Application.Dtos;
using FreeEducation.Services.Order.Application.Mapping;
using FreeEducation.Services.Order.Application.Queries;
using FreeEducation.Services.Order.Infrastructure;
using FreeEducation.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (orders.Count <1)
            {
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }
            var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return ResponseDto<List<OrderDto>>.Success(orderDto, 200);
        }
    }
}
