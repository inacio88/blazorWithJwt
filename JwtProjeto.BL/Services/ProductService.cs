using JwtProjeto.BL.Repositories;
using JwtProjeto.Models.Models;

namespace JwtProjeto.BL.Services;

public class ProductService(IProductRepository productRepository) : IProductService 
{
    public Task<List<ProductModel>> GetProducts()
    {
        return productRepository.GetProducts();
    }
}