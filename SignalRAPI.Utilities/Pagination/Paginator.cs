using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Utilities.Pagination
{
    public static class Paginator
    {
        public static async Task<PaginatorHelper<IEnumerable<TDestination>>> PaginateAsync<TSource, TDestination>
            (this IQueryable<TSource> querable, int pageSize, int pageNumber, IMapper mapper)
            where TSource : class
            where TDestination : class
        {
            var count = querable.Count();
            var pageResult = new PaginatorHelper<IEnumerable<TDestination>>
            {
                PageSize = (pageSize > 10 || pageSize < 1) ? 10 : pageSize,
                CurrentPage = pageNumber > 1 ? pageNumber : 1,
                PreviousPage = pageNumber > 0 ? pageNumber - 1 : 0
            };
            pageResult.NumberOfPages = count % pageResult.PageSize != 0
                    ? count / pageResult.PageSize + 1
                    : count / pageResult.PageSize;
            var sourceList = await querable.Skip(pageResult.CurrentPage - 1).Take(pageResult.PageSize).ToListAsync();
            var destinationList = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(sourceList);
            pageResult.PageItems = destinationList;
            return pageResult;
        }
    }
}
