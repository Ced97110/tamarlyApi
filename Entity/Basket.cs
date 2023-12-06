using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Book book)
        {
            bool bookExistsInBasket = Items.Any(item => item.BookId == book.Id);

            if (!bookExistsInBasket)
            {
                Items.Add(new BasketItem{Book = book});
            }

            var existingItem = Items.FirstOrDefault(item => item.BookId == book.Id);
          
        }

        public void RemoveItem(int bookId)
        {
            var item = Items.FirstOrDefault(item => item.BookId == bookId);
            if (item == null) return;
            Items.Remove(item);
        }
    }
}
