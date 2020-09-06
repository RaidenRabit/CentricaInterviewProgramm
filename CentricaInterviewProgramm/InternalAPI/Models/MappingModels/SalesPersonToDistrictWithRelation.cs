using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI.Models
{
    public class SalesPersonToDistrictWithRelation
    {
        public int SalesPersonToDistrictId { get; set; }
        public int SalesPersonId { get; set; }
        public int DistrictId { get; set; }
        public string RelationName { get; set; }
    }
}
