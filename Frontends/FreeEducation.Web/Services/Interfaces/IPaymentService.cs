using FreeEducation.Web.Models.FakePayments;

namespace FreeEducation.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput input);
    }
}
