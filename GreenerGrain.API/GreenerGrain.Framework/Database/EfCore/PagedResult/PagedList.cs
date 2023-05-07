using API.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenerGrain.Framework.Database.EfCore.PagedResult
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecords { get; private set; }
        public int RecordsOnPage { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalRecords = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            RecordsOnPage = items.Count;

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, IApiContext _apiContext)
        {
            var pageNumber = _apiContext.PagingContext.RequestPaging.Page;
            var pageSize = _apiContext.PagingContext.RequestPaging.PageSize;

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            //Up to maximun of 100 items per page
            if (pageSize > 100)
            {
                pageSize = 100;
            }

            _apiContext.PagingContext.ResponsePaging.SetValues((pageNumber - 1) * pageSize, pageSize, count);

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
