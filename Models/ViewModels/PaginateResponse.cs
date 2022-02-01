using System.Collections.Generic;
using System.Linq;

namespace Advantage.API.Models.ViewModels
{
    public class PaginateResponse<T>
    {
        public PaginateResponse(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            Data = data.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            Total = Data.Count();
        }
        
        
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
        
    }
}