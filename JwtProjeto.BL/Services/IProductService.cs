using JwtProjeto.Models.Models;

namespace JwtProjeto.BL.Services;

public interface IProductService
{
    Task CreateProduct(ProductModel productModel);
    Task DeleteProduct(int id);
    Task<ProductModel> GetProduct(int id);
    Task<List<ProductModel>> GetProducts();
    Task<bool> ProductModelExists(int id);
    Task UpdateProduct(ProductModel productModel);
}