using System;
using System.Threading.Tasks;
using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Api.Models.Requests;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    public class HomeControllerTests 
    {
        private readonly ControllerTestFixture _fixture;

        [Fact]
        public async Task CalculateInsurance_GivenNegativeProductId_ShouldThrowError()
        {
            var _productService = new Mock<IProductService>();
            var _insuranceService = new Mock<IInsuranceService>();
            var sut = new ProductController(_productService.Object, _insuranceService.Object);
            var request = new InsuranceRequest
            {
                ProductId = -1
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.CalculateInsurance(request));
        }

    [Fact]
    public async Task CalculateInsurance_GivenProductIdEqualsToZero_ShouldThrowError()
    {
        var _productService = new Mock<IProductService>();
        var _insuranceService = new Mock<IInsuranceService>();
        var sut = new ProductController(_productService.Object, _insuranceService.Object);
        var request = new InsuranceRequest
        {
            ProductId = 0
        };

        await Assert.ThrowsAsync<ArgumentException>(async () => await sut.CalculateInsurance(request));
    }
    }

public class ControllerTestFixture: IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string) context.Request.RouteValues["id"]);
                            var product = new
                                          {
                                              id = productId,
                                              name = "Test Product",
                                              productTypeId = 1,
                                              salesPrice = 750
                                          };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}