using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBackEnd.Domain;
using TestBackEnd.Interface;
namespace TestBackEnd.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;
        public ProductApiController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var dataResult = await _ProductRepository.Gets();
                return Ok(new ApiSuccessResult<IEnumerable<Product>>(dataResult, "lấy danh sách sản phẩm thành công!"));
            }
            catch (Exception ex)
            {
                return Ok(new ApiErrorResult<IEnumerable<Product>>($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            try
            {
                var data = await _ProductRepository.Create(request);
                if (data != null) {
                    return Ok(new ApiSuccessResult<Product>(data, "Thêm mới sản phẩm thành công!"));

                }
                else
                {
                    return Ok(new ApiSuccessResult<Product>(data, "Thêm mới sản phẩm không thành công!"));

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Product>($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product request)
        {
            try
            {
                var data = await _ProductRepository.Update(request);
                if (data != null)
                {
                    return Ok(new ApiSuccessResult<Product>(data, "Cập nhật sản phẩm thành công!"));

                }
                else
                {
                    return Ok(new ApiSuccessResult<Product>(data, "Cập nhật sản phẩm không thành công!"));

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Product>($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await _ProductRepository.Delete(id);

                return Ok(new ApiSuccessResult<Product>(data, "Xóa sản phẩm thành công !"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Product>($"Lỗi: [{ex.Message}]"));
            }
        }
    }
}