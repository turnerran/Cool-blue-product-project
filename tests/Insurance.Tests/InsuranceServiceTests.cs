using System;
using Insurance.Api.Consts;
using Insurance.Api.Models;
using Insurance.Api.Services;
using WebApi.Services;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceServiceTests
    {
        [Fact]
        public async void CreateProduct_GivenSalesPriceLowerThenTreshhold_ShouldReturnInsuranceZero()
        {
            var productDetails = new ProductDetails
            {
                Id = 2,
                Name = "some-name",
                ProductTypeId = "22",
                SalesPrice = 499,
            };

            var insuranceService = new InsuranceService();
            var insuranceValue = insuranceService.GetInsuranceValue(productDetails);

            Assert.True(insuranceValue == 0);
        }

        [Fact]
        public async void CreateProduct_GivenSalesPriceLargerThenTreshholdAndProductHasInsurance_ShouldReturnInsuranceValue()
        {
            var productDetails = new ProductDetails
            {
                Id = 2,
                Name = "some-name",
                ProductTypeId = "22",
                SalesPrice = 501,
                ProductTypeHasInsurance = true
            };

            var insuranceService = new InsuranceService();
            var insuranceValue = insuranceService.GetInsuranceValue(productDetails);

            Assert.True(insuranceValue == ConstInsuranceValues.MidInsuranceValue);
        }

        [Fact]
        public async void CreateProduct_GivenSalesPriceLargerThenMaximumAndProductHasInsurance_ShouldReturnMaxInsuranceValue()
        {
            var productDetails = new ProductDetails
            {
                Id = 2,
                Name = "some-name",
                ProductTypeId = "22",
                SalesPrice = 2001,
                ProductTypeHasInsurance = true
            };

            var insuranceService = new InsuranceService();
            var insuranceValue = insuranceService.GetInsuranceValue(productDetails);

            Assert.True(insuranceValue == ConstInsuranceValues.MaxInsuranceValue);
        }

        [Fact]
        public async void CreateProduct_GivenLapTopProduct_ShouldReturnSpecialInsuranceValue()
        {
            var productDetails = new ProductDetails
            {
                Id = 2,
                Name = "Laptop",
                ProductTypeName = "Laptops",
                ProductTypeId = "22",
                SalesPrice = 2001,
                ProductTypeHasInsurance = true
            };

            var insuranceService = new InsuranceService();
            var insuranceValue = insuranceService.GetInsuranceValue(productDetails);

            Assert.True(insuranceValue == ConstInsuranceValues.MaxInsuranceValue + ConstInsuranceValues.MinInsuranceValue);
        }
    }
}