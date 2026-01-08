using SSDKTB4.WebApi.Controllers;

namespace SSDKTB4.WebApi.services
{
    public interface IProductService
    {
        Task<ProductCreateResponseModel> CreateProductAsync(ProductCreateRequestModel requestModel);
        Task<ProductDeleteResponseModel> DeleteProduct(int id);
        Task<ProductGetResponseModel> GetProduct(int id);
        Task<ProductGetListResponseModel> GetProductsAsync(int pageSize, int pageNo);
        Task<ProductPatchResponseModel> PatchProduct(int id, ProductPatchRequestModel requestModel);
        Task<ProductUpdateResponseModel> UpdateProduct(int id, ProductUpdateRequestModel requestModel);
    }
}