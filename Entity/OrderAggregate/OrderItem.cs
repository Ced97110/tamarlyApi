using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity.OrderAggregate
{
    public class OrderItem
    {
        public int Id { get; set; }
        public BookItemOrdered ItemOrdered { get; set; }
        public long Price { get; set; }
       
    }
}