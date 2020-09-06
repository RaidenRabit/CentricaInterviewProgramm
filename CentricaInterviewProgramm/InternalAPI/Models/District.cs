
using System.Collections.Generic;

namespace InternalAPI.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public List<Store> Stores { get; set; }
        public List<SalesPersonWithRelation> Spwr { get; set; }
    }

    public class SalesPersonWithRelation
    {
        public SalesPerson SalesPerson { get; set; }
        public string RelationName { get; set; }
    }
}
