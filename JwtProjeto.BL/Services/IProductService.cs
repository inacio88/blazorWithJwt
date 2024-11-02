using JwtProjeto.Models.Models;

namespace JwtProjeto.BL.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetProducts();
}