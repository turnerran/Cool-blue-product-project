using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Insurance.Api.Controllers;
using Newtonsoft.Json;

namespace Insurance.Api
{
    public static class BusinessRules
    {
        /*
        private const string baseAddress = "http://localhost:5002";
        public static void GetProductType(int productID, ref HomeController.InsuranceDto insurance)
        {
            HttpClient client = new HttpClient{ BaseAddress = new Uri(baseAddress)};
            string json = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
            var collection = JsonConvert.DeserializeObject<dynamic>(json);

            json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            int productTypeId = product.productTypeId;

            insurance = new HomeController.InsuranceDto();

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].id == productTypeId && collection[i].canBeInsured)
                {
                    insurance.ProductTypeName = collection[i].name;
                    insurance.ProductTypeHasInsurance = true;
                }
            }
        }

        public static void GetSalesPrice(int productID, ref HomeController.InsuranceDto insurance)
        {
            HttpClient client = new HttpClient{ BaseAddress = new Uri(baseAddress)};
            string json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            insurance.SalesPrice = product.salesPrice;
        }
        */
    }
}