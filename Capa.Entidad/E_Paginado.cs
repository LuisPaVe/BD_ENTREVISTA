using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Entidad
{
    public class E_Paginado<T>
    {
        public List<T> Data { get; set; } = new List<T>();

        public int TotalRecords { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0)
                    return 0;

                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }
    }
}
