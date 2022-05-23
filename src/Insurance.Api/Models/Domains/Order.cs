using Insurance.Api.Consts;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Models.Domains
{
    public class Order
    {
        public long Id { get; set; }
        public double TotalInsuranceCost => CalculateInsurance();
        public List<ProductDetails> Products { get; set; }
        private bool IsDigitalCamerasDiscount()
        {
            return Products.Count(x => x.ProductTypeName == "Digital cameras") > 1;
        }
        private double CalculateInsurance()
        {
            var sum = Products.Sum(x => x.ProductTypeHasInsurance ? x.InsuranceValue : 0);
            sum += IsDigitalCamerasDiscount() ? ConstInsuranceValues.MinInsuranceValue : 0;

            return sum; 
        }
    }
}
