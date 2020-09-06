using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI.Models
{
    public class AddSalesPersonToDistrictModel
    {
        public int SalesPersonId { get; set; }
        public int DistrictId { get; set; }
        public int RelationTypeId { get; set; }
    }
}
