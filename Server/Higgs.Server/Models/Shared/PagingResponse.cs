using System.Collections.Generic;

namespace Higgs.Server.Models.Shared
{
    public class PagingResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
    }
}
