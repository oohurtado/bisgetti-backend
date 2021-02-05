using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Common
{
    public class PageData<T>
    {
        public List<T> Data { get; set; }
        public int GrandTotal { get; set; }

        public PageData()
        {
        }

        public PageData(List<T> data, int grandTotal)
        {
            Data = data;
            GrandTotal = grandTotal;
        }
    }
}
