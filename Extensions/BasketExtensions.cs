using API.DTOs;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class BasketExtensions
    {
        public static BasketDto MapBasketToDto(this Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    BookId = item.BookId,
                    Title  = item.Book.Title,
                    Price = item.Book.Price,
                    imageUrl = item.Book.ImageUrl,
                }).ToList()
            };
        }

        public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query, string buyerId)
        {
            return query
                .Include(i => i.Items)
                .ThenInclude(p => p.Book)
                .Where(basket => basket.BuyerId == buyerId);
        }
    }
}