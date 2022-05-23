using System;
using System.Threading.Tasks;
using Insurance.Api.Models;
using Insurance.Api.Models.Requests;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class ProductController: Controller
    {
        private IProductService _productService;
        private IInsuranceService _insuranceService;
        public ProductController(IProductService productService, IInsuranceService insuranceService)
        {
            _productService = productService;
            _insuranceService = insuranceService;
        }

        [HttpPost]
        [Route("api/insurance/product")]
        public async Task<ProductDetails> CalculateInsurance([FromBody] InsuranceRequest insuranceRequest)
        {
            int productId = insuranceRequest.ProductId;
            if (productId <= 0)
            {
                throw new ArgumentException("Product id must be positive number");
            }

            var product = await _productService.GetProductType(productId);
            if (product== null)
            {
                throw new ArgumentException($"No such product exist with id {productId}");
            }

            product.InsuranceValue = _insuranceService.GetInsuranceValue(product);

            return product;
        }
    }
}