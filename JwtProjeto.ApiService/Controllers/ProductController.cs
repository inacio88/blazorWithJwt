using JwtProjeto.BL.Services;
using Microsoft.AspNetCore.Mvc;
using JwtProjeto.Models.Models;
namespace JwtProjeto.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : Controller
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponseModel>> GetProducts()
        {
            var products = await productService.GetProducts();
            return Ok(new BaseResponseModel{Success = true, Data = products});
        }

    }
}
