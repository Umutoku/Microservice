using FreeEducation.Web.Models.FakePayments;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ReceivePayment(PaymentInfoInput input)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments",input);
            return response.IsSuccessStatusCode;
        }
    }
}
