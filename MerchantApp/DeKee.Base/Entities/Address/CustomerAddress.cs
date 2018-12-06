using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Address
{
    public class CustomerAddress : SupervisedEntityGeneric<long>
    {
        public long? PointId { get; set; }
        [ForeignKey("PointId")]
        public Point Point { get; set; }

        public long? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }

        public long? CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public long? RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer.Customer Customer { get; set; }
    }

    public class Point : SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class District : SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class City : SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Region : SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
