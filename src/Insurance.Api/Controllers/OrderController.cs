using System;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Models;
using Insurance.Api.Models.Domains;
using Insurance.Api.Models.Requests;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace Insurance.Api.Controllers
{
    public class OrderController: Controller
    {
        private IProductService _productService;
        private ICacheService _cacheService;
        private readonly IMapper _mapper;
        public OrderController(IProductService productService, ICacheService cacheService,
            IMapper mapper)
        {
            _productService = productService;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/insurance/order")]
        public void CreateOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var accountId = orderCreateRequest.AccountId;
            if (accountId <= 0)
            {
                throw new ArgumentException("account id must be positive number");
            }

            var order = _cacheService.GetOrderById(accountId);
            if (order != null)
            {
                throw new ArgumentException($"Order allready exists for account id {accountId}");
            }

            _cacheService.CreateOrder(accountId);
        }

        [HttpPost]
        [Route("api/insurance/order/{id}")]
        public async Task<ProductDetails> AddProductToOrder(long id, [FromBody] ProductOrderRequest productOrderRequest)
        {
            var productId = productOrderRequest.ProductId;
            if (id <= 0)
            {
                throw new ArgumentException("id must be positive number");
            }

            var order = _cacheService.GetOrderById(id);
            if (order == null)
            {
                throw new ArgumentException($"No order exists for id {id}");
            }
            var product = await _productService.GetProductById(productId);
            _cacheService.AddProductToOrder(id, product);
            
            return product;
        }

        [HttpGet]
        [Route("api/insurance/order/{id}")]
        public Order GetOrder(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("account id must be positive number");
            }

            var order = _cacheService.GetOrderById(id);
            if (order == null)
            {
                throw new ArgumentException($"No order exists for id {id}");
            }

            return order;
        }
    }
}