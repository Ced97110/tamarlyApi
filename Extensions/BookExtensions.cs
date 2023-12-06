using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;


namespace API.Extensions
{
    public static class BookExtensions
    {
        public static IQueryable<Book> Sort(this IQueryable<Book> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query.OrderBy(p => p.Title);
            }

            switch (orderBy)
            {
                case "price":
                    return query.OrderBy(p => p.Price);
                case "priceDesc":
                    return query.OrderByDescending(p => p.Price);
                case "rating":
                    return query.OrderByDescending(p => p.Rating);
                case "arrivals":
                    return query.OrderByDescending(p => p.PublicationDate);
                default:
                    return query;
            }
        }



        public static IQueryable<Book> Search(this IQueryable<Book> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Book> Filter(this IQueryable<Book> query, string category, string rating, string language)
        {
            var filters = new[] { category, rating, language };

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(p => p.Category.ToLower().Contains(filter) || p.Rating.ToLower().Contains(filter) || p.Language.ToLower().Contains(filter));
                }
            }

            return query;
        }

    }


}