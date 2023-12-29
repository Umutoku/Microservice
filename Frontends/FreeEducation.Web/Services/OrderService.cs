using FreeEducation.Shared.Dtos;
using FreeEducation.Shared.Services;
using FreeEducation.Web.Models;
using FreeEducation.Web.Models.FakePayments;
using FreeEducation.Web.Models.Orders;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.Get();
            var payment = new PaymentInfoInput()
            {
                CardName = input.CardName,
                CardNumber = input.CardNumber,
                Expiration = input.Expiration,
                CVV = input.CVV,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment= await _paymentService.ReceivePayment(payment);
            if(!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödeme Alınamadı" ,Successfull=false};
            }
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new Models.AddressCreateInput()
                {
                    Province = input.Province,
                    District = input.District,
                    Line = input.Line,
                    Street = input.Street,
                    ZipCode = input.ZipCode,
                },
                
            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.EducationId, Price = x.GetCurrentPrice, ProductName = x.EducationName, PictureUrl = "" };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders",orderCreateInput);

            if(!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı", Successfull = false };
            }
            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.Successfull = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;

            
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.Get();
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput() { Province =  input.Province, District = input.District, Street = input.Street, Line = input.Line, ZipCode = input.ZipCode },
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.EducationId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.EducationName };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = input.CardName,
                CardNumber = input.CardNumber,
                Expiration = input.Expiration,
                CVV = input.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", isSuccessful = false };
            }

            await _basketService.Delete();
            return new OrderSuspendViewModel() { isSuccessful = true };
        }
    }
}
