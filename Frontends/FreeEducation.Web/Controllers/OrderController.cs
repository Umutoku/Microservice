using FreeEducation.Web.Models.Orders;
using FreeEducation.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
            var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput);
            if(!orderStatus.isSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return RedirectToAction(nameof(Checkout));
            }
            //return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId});
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1,1111)});

        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
        
        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrder());
        }
    }
}
