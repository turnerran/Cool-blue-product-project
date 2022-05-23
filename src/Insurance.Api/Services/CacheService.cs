using Insurance.Api.Models;
using Insurance.Api.Models.Domains;
using System;
using System.Collections.Generic;

namespace WebApi.Services
{
    public interface ICacheService
    {
        Order GetOrderById(long id);
        void CreateOrder(long accountId);
        void AddProductToOrder(long accountId, ProductDetails product);
        bool Remove(long id);
    }

    public class CacheService : ICacheService
    {
        private static Dictionary<long, List<ProductDetails>> _cache = new Dictionary<long, List<ProductDetails>>();

        public void AddProductToOrder(long accountId, ProductDetails productDetails)
        {
            if (!_cache.ContainsKey(accountId))
            {
                throw new InvalidOperationException($"accountId {accountId} doesn't have an order");
            }

            if (productDetails.SalesPrice <= 0)
            {
                throw new ArgumentException("sales price cannot be negative");
            }

            _cache[accountId].Add(productDetails);
        }

        public Order GetOrderById(long id)
        {
            if (_cache.ContainsKey(id))
            {
                {
                    return new Order
                    {
                        Id = id,
                        Products = _cache[id]
                    };
                }
            }

            return null;
        }

        public bool Remove(long id)
        {
            if (_cache.ContainsKey(id))
            {
                _cache.Remove(id);
                return true;
            }
            return false;
        }

        public void CreateOrder(long accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException($"{accountId} must be positive");
            }

            if (_cache.ContainsKey(accountId))
            {
                throw new InvalidOperationException($"{accountId} allready has an existing order");
            }
            _cache.Add(accountId, new List<ProductDetails>());
        }
    }
}