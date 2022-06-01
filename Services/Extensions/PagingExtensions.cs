using Data.Entities;
using Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Extensions
{
    public static class PagingExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int page, int pageSize) where T : Entity
        {
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, items.Count, page, pageSize);
        }
    }
}
