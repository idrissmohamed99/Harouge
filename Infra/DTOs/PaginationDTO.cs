using System.Collections.Generic;

namespace Infra.DTOs
{
    public class PaginationDto<T>
    {
        public ICollection<T> Data { get; set; } = new HashSet<T>();
        public int PageCount { get; set; }
    }
}
