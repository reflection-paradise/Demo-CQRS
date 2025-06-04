using Application.Command;
using Application.RespondModels;
using Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GetAllProductHandler _handler;
        private readonly CreateProductHandler _create_handler;

        public ProductController(GetAllProductHandler handler, CreateProductHandler create_handler)
        {
            _handler = handler;
            _create_handler = create_handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _handler.GetAllProductAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductController(CreateProductDTO data)
        {
            try
            {
                var result = await _create_handler.CreateHandler(data);

                var response = new MessageRespondDTO<object>(
                    data: result,
                    status: true,
                    message: "Tạo sản phẩm thành công."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new MessageRespondDTO<object>(
                    data: null,
                    status: false,
                    message: $"Đã xảy ra lỗi: {ex.Message}"
                );

                return StatusCode(500, errorResponse);
            }
        }

    }
}
