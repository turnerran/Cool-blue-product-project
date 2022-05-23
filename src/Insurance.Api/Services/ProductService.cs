using Insurance.Api.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Insurance.Api.Services
{
    public interface IProductService
    {
        public Task<ProductDetails> GetProductType(int productID);
        public Task<float> GetSalesPrice(int productID);
        public Task<ProductDetails> GetProductById(int productId);
    }
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private IInsuranceService _insuranceService;
        public ProductService(IHttpClientFactory httpClientFactory, IInsuranceService insuranceService)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("action");
            _httpClient.BaseAddress = BaseAddress;
            _insuranceService = insuranceService;
        }

        private readonly Uri BaseAddress = new Uri("http://localhost:5002");
        public async Task<ProductDetails> GetProductType(int productID)
        {
            var productTypesRes = await _httpClient.GetAsync("/product_types");
            var productTypesContent = await productTypesRes.Content.ReadAsStringAsync();
            var productTypes = JsonConvert.DeserializeObject<ProductType[]>(productTypesContent);
            var productType = productTypes.FirstOrDefault();

            if (productType == null)
            {
                throw new ArgumentException("No such product type exist");
            }

            var productData = await _httpClient.GetAsync(string.Format("/products/{0:G}", productID));
            var productDataContent = await productData.Content.ReadAsStringAsync();
            var productInsuranceDetails = JsonConvert.DeserializeObject<ProductDetails>(productDataContent);

            productInsuranceDetails.ProductTypeName = productType.Name;
            productInsuranceDetails.ProductTypeHasInsurance = productType.CanBeInsured;

            return productInsuranceDetails;
        }

        public async Task<float> GetSalesPrice(int productID)
        {
            string json = await _httpClient.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            return product.salesPrice;
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            string json = await _httpClient.GetAsync(string.Format("/products/{0:G}", productId)).Result.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDetails>(json);
            if (product.Id == 0)
            {
                throw new ArgumentException($"No such product exist with id {productId}");
            }

            var productTypesRes = await _httpClient.GetAsync($"/product_types/{product.ProductTypeId}");
            var productTypesContent = await productTypesRes.Content.ReadAsStringAsync();
            var productType = JsonConvert.DeserializeObject<ProductType>(productTypesContent);

            product.ProductTypeHasInsurance = productType.CanBeInsured;
            product.ProductTypeName = productType.Name;
            product.InsuranceValue = _insuranceService.GetInsuranceValue(product);

            return product;
        }
    }
}
