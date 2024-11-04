using JwtProjeto.Database.Data;
using JwtProjeto.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtProjeto.BL.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<ProductModel> CreateProduct(ProductModel productModel)
    {
        dbContext.Add(productModel);
        await dbContext.SaveChangesAsync();
        return productModel;
    }

    public  Task<ProductModel> GetProduct(int id)
    {
        return dbContext.Products.FirstAsync(x => x.Id == id);
    }

    public Task<List<ProductModel>> GetProducts()
    {
        return dbContext.Products.ToListAsync();
    }

    public Task<bool> ProductModelExists(int id)
    {
        return dbContext.Products.AnyAsync(x => x.Id == id);
    }

    public async Task UpdateProduct(ProductModel productModel)
    {
        dbContext.Entry(productModel).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}