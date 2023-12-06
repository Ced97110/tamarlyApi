using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;



namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public BasketController(StoreContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContext = httpContextAccessor;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
           
           var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) return NotFound();

           return MapBasketToDto(basket);
        }

        

        [HttpPost]
        public async  Task<ActionResult<BasketDto>> AddItemToBasket(int bookId)
        {
            
            var basket = await RetrieveBasket(GetBuyerId());

            
            if (basket == null) basket =  CreateBasket();

            var book = await _context.Books.FindAsync(bookId);

            if (book == null) return NotFound();
            
            basket.AddItem(book);

            var result = await _context.SaveChangesAsync() > 0;
            if(result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

            Console.Write(Request.Headers);

            return BadRequest(new ProblemDetails{Title = "Problem saving the item"});
        }


        [HttpDelete]
        public async Task<ActionResult<BasketDto>> RemoveBasketItem(int bookId)
        {
            
            var basket = await RetrieveBasket(GetBuyerId());

           if (basket == null) return NotFound();

           basket.RemoveItem(bookId);

           var result = await _context.SaveChangesAsync() > 0;


            if (result)
            {
                
                return Ok(); // Return the updated basket
            }

            return BadRequest(new ProblemDetails{Title = "Problem removing item"});


        }


        private string GetBuyerId()
        {
            return  _httpContext.HttpContext.User.Identity.Name ?? Request.Cookies["buyerId"];
            
        }



        private  Basket CreateBasket()
        {
            var buyerId = _httpContext.HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }

            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }



        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Book)
                .FirstOrDefaultAsync(basket => basket.BuyerId == buyerId);
        }



        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    BookId = item.BookId,
                    Title = item.Book.Title,
                    Author = item.Book.Author,
                    Price = item.Book.Price,
                    imageUrl = item.Book.ImageUrl,
                    

                }).ToList()
            };
        }

    }
}