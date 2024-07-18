using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBackEnd.Domain;
using TestBackEnd.Interface;
using TestBackEnd.Repository;
namespace TestBackEnd.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryApiController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var dataResult = await _CategoryRepository.Gets();
                return Ok(new ApiSuccessResult<IEnumerable<Category>>(dataResult, "lấy danh sách thể loại thành công!"));
            }
            catch (Exception ex)
            {
                return Ok(new ApiErrorResult<IEnumerable<Category>> ($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            try
            {
                var data = await _CategoryRepository.Create(request);

                return Ok(new ApiSuccessResult<Category>(data, "Thêm mới thể loại thành công!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Category>($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category request)
        {
            try
            {
                var data = await _CategoryRepository.Update(request);
                if (data != null)
                {
                    return Ok(new ApiSuccessResult<Category>(data, "Cập nhật thể loại thành công !"));

                }
                else
                {
                    return Ok(new ApiSuccessResult<Category>(data, "Cập nhật thể loại không thành công !"));

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Category>($"Lỗi: [{ex.Message}]"));
            }
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await _CategoryRepository.Delete(id);

                if (data != null)
                {
                    return Ok(new ApiSuccessResult<Category>(data, "Xóa thể loại thành công !"));

                }
                else
                {
                    return Ok(new ApiSuccessResult<Category>(data, "Xóa thể loại không thành công !"));

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Category>($"Lỗi: [{ex.Message}]"));
            }
        }
    }
}