using JwtProjeto.Database.Data;
using JwtProjeto.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtProjeto.BL.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public Task<List<ProductModel>> GetProducts()
    {
        return dbContext.Products.ToListAsync();
    }
}