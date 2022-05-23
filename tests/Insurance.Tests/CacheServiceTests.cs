using System;
using Insurance.Api.Models;
using WebApi.Services;
using Xunit;

namespace Insurance.Tests
{
    public class CacheServiceTests
    {
        [Fact]
        public async void CreatingOrder_GivenPositiveAccountId_ShouldReturnOrder()
        {
            var accountId = 32;
            var cacheService = new CacheService();
            cacheService.CreateOrder(accountId);

            var cachedOrder = cacheService.GetOrderById(accountId);
            Assert.True(cachedOrder.Id == accountId);
            Assert.True(cachedOrder.Products.Count == 0);
        }

        [Fact]
        public async void CreatingOrder_WithNegativeProductPrice_ShouldThrowError()
        {
            var accountId = 22;
            var cacheService = new CacheService();
            cacheService.CreateOrder(accountId);

            var cachedOrder = cacheService.GetOrderById(accountId);

            var productDetails = new ProductDetails
            {
                Id = 2,
                Name = "Laptop",
                ProductTypeId = "22",
                SalesPrice = -123
            };

            Assert.Throws<ArgumentException>(() => cacheService.AddProductToOrder(accountId, productDetails));
        }

        [Fact]
        public async void CreatingOrder_GivenNegativeAccountId_ShouldThrowError()
        {
            var accountId = -22;
            var cacheService = new CacheService();

            Assert.Throws<ArgumentException>(() => cacheService.CreateOrder(accountId));
        }

        [Fact]
        public async void CreatingOrder_GivenZeroAccountId_ShouldThrowError()
        {
            var accountId = 0;
            var cacheService = new CacheService();

            Assert.Throws<ArgumentException>(() => cacheService.CreateOrder(accountId));
        }
    }
}