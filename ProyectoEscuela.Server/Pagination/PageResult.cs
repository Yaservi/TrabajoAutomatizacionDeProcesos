using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProyectoEscuela.Server.Pagination
{
    public class PageResult<T>
    {
        public PageResult() 
        {
        }

        public PageResult(IEnumerable<T> items, int totalItems, int currentPages, int pageZise ) 
        {
            Items = items;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageZise);
            CurrentPage = currentPages;
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
