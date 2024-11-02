using JwtProjeto.Models.Models;

namespace JwtProjeto.BL.Repositories;

public interface IProductRepository
{
    Task<List<ProductModel>> GetProducts();
}