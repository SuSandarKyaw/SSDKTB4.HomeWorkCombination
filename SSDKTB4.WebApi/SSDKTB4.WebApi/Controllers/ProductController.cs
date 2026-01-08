using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSDKTB4.WebApi.Database.AppDbContextModels;
using SSDKTB4.WebApi.services;

namespace SSDKTB4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetProducts(int pageSize, int pageNo)
        {
			var result = await _productService.GetProductsAsync( pageSize, pageNo);
		    return Ok(result);
        }
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
		var result =await _productService.GetProduct(id);
		if (result.IsSuccess)
		{
			return NotFound(result);
		}
		return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(ProductCreateRequestModel requestModel)
		{
		var result =await _productService.CreateProductAsync(requestModel);

		return Ok(result);
		}

        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, ProductUpdateRequestModel requestModel)
		{
		var result = await _productService.UpdateProduct(id,requestModel);
		return Ok(result);
		}
        [HttpPatch("{id}")]
		public async Task<IActionResult> PatchProduct(int id, ProductPatchRequestModel requestModel)
		{

		var result = await _productService.PatchProduct(id,requestModel);
		return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
		var result =await _productService.DeleteProduct(id);

		return Ok(result);
		}
}

public class ProductGetListResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
	public List<Tbl3Product> Products { get; set; } = null!;
}
public class ProductGetResponseModel
{

	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
	public int ProductId { get; set; }
	public string ProductName { get; set; } = null!;
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	public DateTime CreatedDateTime { get; set; }

}
public class ProductCreateRequestModel
{
	public string ProductName { get; set; } = null!;

	public decimal Price { get; set; }

	public int Quantity { get; set; }
}

public class ProductCreateResponseModel
{

	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
	//public int ProductId { get; set; }
	//public string ProductName { get; set; } = null!;
	//public decimal Price { get; set; }
	//public int Quantity { get; set; }
	//public DateTime CreatedDateTime { get; set; }
}

public class ProductUpdateRequestModel
{
	public string ProductName { get; set; } = null!;

	public decimal Price { get; set; }

	public int Quantity { get; set; }
}

public class ProductUpdateResponseModel
{

	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
	public int ProductId { get; set; }
	public string ProductName { get; set; } = null!;
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	//public DateTime CreatedDateTime { get; set; }
	public DateTime? ModifiedDateTime { get; set; }
}

public class ProductPatchRequestModel
{
	public string? ProductName { get; set; } = null!;

	public decimal? Price { get; set; }

	public int? Quantity { get; set; }
}

public class ProductPatchResponseModel
{

	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
	public int ProductId { get; set; }
	public string ProductName { get; set; } = null!;
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	//public DateTime CreatedDateTime { get; set; }
	public DateTime? ModifiedDateTime { get; set; }
}

public class ProductDeleteResponseModel
{

	public bool IsSuccess { get; set; }
	public string Message { get; set; } = null!;
}