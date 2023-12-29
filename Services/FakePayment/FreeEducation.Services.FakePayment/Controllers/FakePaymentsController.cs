using FreeEducation.Services.FakePayment.Models;
using FreeEducation.Shared.ControllerBases;
using FreeEducation.Shared.Dtos;
using FreeEducation.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = paymentDto.Order.BuyerId,
                Province = paymentDto.Order.Address.Province,
                District = paymentDto.Order.Address.District,
                Street = paymentDto.Order.Address.Street,
                Line = paymentDto.Order.Address.Line,
                ZipCode = paymentDto.Order.Address.ZipCode
            };
            paymentDto.Order.OrderItems.ForEach(item =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                });
            });
            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            
            return CreateActionResultInstance(ResponseDto<NoContent>.Success(200));
        }

    }
}
