﻿using FreeEducation.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Services.Order.Domain.OrderAggregate
{
    public class OrderItem :Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }

        public OrderItem()
        {
            
        }


        public OrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }
        public void UpdateOrderItem(string productName, decimal price, string pictureUrl)
        {
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }
    }
}
