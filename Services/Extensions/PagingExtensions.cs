using Data.Entities;
using Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int page, int pageSize) where T : Entity
        {
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, items.Count, page, pageSize);
        }
    }
}
