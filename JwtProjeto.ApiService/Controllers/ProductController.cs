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

        [HttpPost]
        public async Task<ActionResult<ProductModel>> CreateProduct(ProductModel productModel)
        {
            await productService.CreateProduct(productModel);
            return Ok(new BaseResponseModel{Success = true});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponseModel>> GetProduct(int id)
        {
            var productModel = await productService.GetProduct(id);

            if (productModel is null)
            {
                return Ok(new BaseResponseModel {Success = false, ErrorMessage = "Not found"});
            }

            return Ok(new BaseResponseModel {Success = true, Data = productModel});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel productModel)
        {
            if (id != productModel.Id || !await productService.ProductModelExists(id))
            {
                return Ok(new BaseResponseModel {Success = false, ErrorMessage = "Bad request - id incompativel"});
            }

            await productService.UpdateProduct(productModel);
            return Ok(new BaseResponseModel {Success = true});
        }

    }
}
