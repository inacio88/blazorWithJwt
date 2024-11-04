using JwtProjeto.Models.Models;

namespace JwtProjeto.BL.Repositories;

public interface IProductRepository
{
    Task<ProductModel> CreateProduct(ProductModel productModel);
    Task<ProductModel> GetProduct(int id);
    Task<List<ProductModel>> GetProducts();
    Task<bool> ProductModelExists(int id);
    Task UpdateProduct(ProductModel productModel);
}