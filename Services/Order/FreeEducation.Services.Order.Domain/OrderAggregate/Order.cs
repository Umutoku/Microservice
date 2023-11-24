using FreeEducation.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Services.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get;private set; }
        public Address Address { get;private set; }
        public string BuyerId { get;private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
            
        }
        public Order(string buyerId,Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId,string productName,decimal price,string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, price, pictureUrl);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

        public void UpdateOrderItem(string productId,string productName,decimal price,string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (existProduct)
            {
                var updateOrderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
                updateOrderItem.UpdateOrderItem(productName, price, pictureUrl);
            }
        }

        public void RemoveOrderItem(string productId)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (existProduct)
            {
                var removeOrderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
                _orderItems.Remove(removeOrderItem);
            }
        }
    }
}
