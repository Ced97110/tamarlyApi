using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entity;
using API.Extensions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   
   
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public ProductsController(StoreContext context, IMapper mapper, ImageService imageService)
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


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Book>> CreateProduct([FromForm] CreateProductDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            if (bookDto.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(bookDto.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                book.ImageUrl = imageResult.SecureUrl.ToString();
                book.PublicId = imageResult.PublicId;
            }

            _context.Books.Add(book);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetProduct", new { Id = book.Id }, book);

            return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
        }




        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<Book>> UpdateProduct([FromForm] UpdateProductDto bookDto)
        {
            var book = await _context.Books.FindAsync(bookDto.Id);

            if (book == null) return NotFound();

            _mapper.Map(bookDto, book);

            if (bookDto.File != null)
            {
                var imageUploadResult = await _imageService.AddImageAsync(bookDto.File);

                if (imageUploadResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageUploadResult.Error.Message });

                if (!string.IsNullOrEmpty(book.PublicId))
                    await _imageService.DeleteImageAsync(book.PublicId);

                book.ImageUrl = imageUploadResult.SecureUrl.ToString();
                book.PublicId = imageUploadResult.PublicId;
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok(book);

            return BadRequest(new ProblemDetails { Title = "Problem updating product" });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) return NotFound();

            if (!string.IsNullOrEmpty(book.PublicId))
                await _imageService.DeleteImageAsync(book.PublicId);

            _context.Books.Remove(book);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }
    }
}



        


