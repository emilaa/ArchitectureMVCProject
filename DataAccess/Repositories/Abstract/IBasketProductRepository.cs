﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IBasketProductRepository : IRepository<BasketProduct>
    {
        Task<BasketProduct> GetBasketProducts(int modelId, int basketId);
        Task<bool> DeleteProductAsync(int productdId);
        Task<int> GetUserBasketProductsCount(ClaimsPrincipal user);


    }
}
