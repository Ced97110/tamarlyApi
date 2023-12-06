using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using API.Extensions;
using API.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   
   
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

       
        [HttpGet]  
        public async Task<ActionResult<PagedList<Book>>> GetProducts([FromQuery]BookParams bookParams)
        {
           var query =  _context.Books
                        .Sort(bookParams.OrderBy)
                        .Search(bookParams.SearchTerm)
                        .Filter(bookParams.Category, bookParams.Rating, bookParams.Language)
                        .AsQueryable();

         
           var books = await PagedList<Book>.ToPageList(query, bookParams.PageNumber, bookParams.PageSize);

           Response.AddPaginationHeader(books.MetaData);
           return books;
           
        }


        [HttpGet("{id}")]
        public  async Task<ActionResult<List<Book>>> GetProduct(int id)
        {
            var book =  await _context.Books.FindAsync(id);

            return Ok(book);
        }
   }
}
