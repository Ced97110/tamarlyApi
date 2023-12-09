using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Entity.OrderAggregate
{
    [Owned]
    public class BookItemOrdered
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string imageUrl { get; set; }
    }
} 