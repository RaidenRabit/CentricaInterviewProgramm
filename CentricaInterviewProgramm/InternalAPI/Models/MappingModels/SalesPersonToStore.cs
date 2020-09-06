using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI.Models
{
    public class SalesPersonToStore
    {
        public int SalesPersonToStoreId { get; set; }
        public int SalesPersonId { get; set; }
        public int StoreId { get; set; }
    }
}
