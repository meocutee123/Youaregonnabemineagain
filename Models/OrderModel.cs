using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronic_Store.Models
{
    public class OrderModel
    {
        public IEnumerable<OrderItem> OrderItemsModel { get; set; }
        public OrderItem NonIEOrderItemsModel { get; set; }
    }
}