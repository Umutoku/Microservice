using FreeEducation.Services.Order.Application.Dtos;
using FreeEducation.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Services.Order.Application.Queries
{
    public class GetOrderByUserIdQuery :IRequest<ResponseDto<List<OrderDto>>>
    {
        public string UserId { get; set; }

    }
}
