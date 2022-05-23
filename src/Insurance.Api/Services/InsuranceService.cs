using Insurance.Api.Models;
using Insurance.Api.Consts;

namespace Insurance.Api.Services
{
    public interface IInsuranceService
    {
        public float GetInsuranceValue(ProductDetails product);
    }
    public class InsuranceService : IInsuranceService
    {
        public float GetInsuranceValue(ProductDetails product)
        {
            var insuranceValue = product.InsuranceValue;
            if (product.SalesPrice < ConstPrices.SalesPriceMinPrice)
            {
                insuranceValue = 0;
            }
            else
            {
                if (product.SalesPrice > ConstPrices.SalesPriceMinPrice && product.SalesPrice < ConstPrices.SalesPriceMaxPrice)
                {
                    if (product.ProductTypeHasInsurance)
                    {
                        insuranceValue += ConstInsuranceValues.MidInsuranceValue;
                    }
                }
                else if (product.SalesPrice >= ConstPrices.SalesPriceMaxPrice)
                {
                    if (product.ProductTypeHasInsurance)
                    {
                        insuranceValue += ConstInsuranceValues.MaxInsuranceValue;
                    }
                }
            }

            if (product.ProductTypeName == "Laptops" || product.ProductTypeName == "Smartphones" && product.ProductTypeHasInsurance)
            {
                insuranceValue += ConstInsuranceValues.MinInsuranceValue;
            }

            return insuranceValue;
        }
    }
}
