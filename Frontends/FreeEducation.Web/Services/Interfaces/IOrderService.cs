using FreeEducation.Web.Models.Orders;

namespace FreeEducation.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input);

        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input);

        Task<List<OrderViewModel>> GetOrder();
    }
}
