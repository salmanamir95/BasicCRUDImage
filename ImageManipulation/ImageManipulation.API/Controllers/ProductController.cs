using ImageManipulation.Data.Models;
using ImageManipulation.Data.Models.DTO;
using ImageManipulation.Data.Repositories;
using ImageManipulation.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageManipulation.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IFileService fileService, 
        IProductRepository productRepo, ILogger<ProductController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDTO productToAdd)
        {
            try
            {
                if (productToAdd.ImageFile?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size exceeded from 1MB");
                }
                string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(productToAdd.ImageFile, allowedExtensions);

                var product = new Product
                {
                    ProductName = productToAdd.ProductName,
                    ProductImage = createdImageName
                };
                var createdProduct = await productRepo.AddProductAsync(product);
                return CreatedAtAction(nameof(CreateProduct), createdProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
