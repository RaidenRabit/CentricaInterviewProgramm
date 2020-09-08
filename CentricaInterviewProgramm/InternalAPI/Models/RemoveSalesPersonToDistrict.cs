using System.Collections.Generic;

namespace InternalAPI.Models
{
    public class RemoveSalesPersonToDistrict
    {
        public List<int> SalesPersonIds { get; set; }
        public int DistrictId { get; set; }
    }
}
