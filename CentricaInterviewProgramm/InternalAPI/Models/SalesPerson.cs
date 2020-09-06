using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI.Models
{
    public class SalesPerson
    {
        public int SalesPersonId { get; set; }
        public string SalesPersonName { get; set; }
        public List<Store> Stores { get; set; }
    }
}
