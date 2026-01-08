using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSDKTB4.WebApi.Controllers;
using SSDKTB4.WebApi.Database.AppDbContextModels;
using SSDKTB4.WebApi.services;

namespace SSDKTB4.WebApi.services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;


        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProductGetListResponseModel> GetProductsAsync(int pageSize, int pageNo)
        {
            if (pageNo <= 0)
            {
                return new ProductGetListResponseModel
                {
                    IsSuccess = false,
                    Message = "Page number must be greater than zero.",
                    Products = new List<Tbl3Product>()
                };
            }

            if (pageSize <= 0)
            {
                return new ProductGetListResponseModel
                {
                    IsSuccess = false,
                    Message = "Invalid Page size.",
                    Products = new List<Tbl3Product>()
                };
            }

            var product = await _db.Tbl3Products.OrderByDescending(x => x.ProductId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            var responseModel = new ProductGetListResponseModel
            {
                IsSuccess = true,
                Products = product,
                Message = "Products retrieved successfully.",
            };
            return responseModel;
        }

        public async Task<ProductGetResponseModel> GetProduct(int id)
        {
            var item = await _db.Tbl3Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (item is null)
            {
                return new ProductGetResponseModel
                {
                    IsSuccess = true,
                    Message = "Products retrieved successfully.",
                };
            }

            var responseModel = new ProductGetResponseModel
            {
                IsSuccess = true,
                Message = "Product retrieved successfully.",
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                CreatedDateTime = item.CreatedDateTime
            };
            return responseModel;
        }

        public async Task<ProductCreateResponseModel> CreateProductAsync(ProductCreateRequestModel requestModel)
        {
            Tbl3Product product = new Tbl3Product
            {
                ProductName = requestModel.ProductName,
                Price = requestModel.Price,
                Quantity = requestModel.Quantity,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            };
            _db.Tbl3Products.Add(product);
            var result = await _db.SaveChangesAsync();
            var message = result > 0 ? "Product created successfully." : "Failed to create product.";
            var responseModel = new ProductCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = message,

            };
            return responseModel;
        }

        public async Task<ProductUpdateResponseModel> UpdateProduct(int id, ProductUpdateRequestModel requestModel)
        {
            var product = await _db.Tbl3Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product is null)
            {
                return new ProductUpdateResponseModel
                {

                    IsSuccess = false,
                    Message = "Not found data to update.",
                };
            }
            product.ProductName = requestModel.ProductName;
            product.Price = requestModel.Price;
            product.Quantity = requestModel.Quantity;
            product.ModifiedDateTime = DateTime.Now;
            var result = _db.SaveChanges();
            var message = result > 0 ? "Product updated successfully." : "Failed to update product.";
            var responseModel = new ProductUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = message,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                ModifiedDateTime = product.ModifiedDateTime

            };
            return responseModel;
        }


        public async Task<ProductPatchResponseModel> PatchProduct(int id, ProductPatchRequestModel requestModel)
        {
            var product = await _db.Tbl3Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product is null)
            {
                return new ProductPatchResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                };
            }
            ;
            if (requestModel.ProductName is not null)
            {
                product.ProductName = requestModel.ProductName;
            }
            if (requestModel.Price is not null && requestModel.Price > 0)
            {
                product.Price = Convert.ToDecimal(requestModel.Price);
            }
            if (requestModel.Quantity is not null && requestModel.Quantity > 0)
            {
                product.Quantity = Convert.ToInt32(requestModel.Quantity);
            }
            product.ModifiedDateTime = DateTime.Now;
            var result = _db.SaveChanges();
            var message = result > 0 ? "Product patched successfully." : "Failed to patch product.";
            var responseModel = new ProductPatchResponseModel
            {
                IsSuccess = result > 0,
                Message = message,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                ModifiedDateTime = product.ModifiedDateTime
            };
            return responseModel;
        }

        public async Task<ProductDeleteResponseModel> DeleteProduct(int id)
        {
            var product = await _db.Tbl3Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product is null)
            {
                return new ProductDeleteResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                };
            }
            product.IsDelete = true;
            var result = _db.SaveChanges();
            var message = result > 0 ? "Product deleted successfully." : "Failed to delete product.";
            var responseModel = new ProductDeleteResponseModel
            {
                IsSuccess = result > 0,
                Message = message
            };
            return responseModel;

        }

    }

}
