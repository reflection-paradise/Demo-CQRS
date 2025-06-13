using Application.Command;
using Application.RespondModels;
using Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GetAllProductHandler _handler;
        private readonly CreateProductHandler _create_handler;
        private readonly UpdateProductHandler _update_handler;
        private readonly DeleteProductHandler _delete_handler;

        public ProductController(GetAllProductHandler handler, CreateProductHandler create_handler, UpdateProductHandler update_handler, DeleteProductHandler delete_handler)
        {
            _handler = handler;
            _create_handler = create_handler;
            _update_handler = update_handler;
            _delete_handler = delete_handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew(); 

            try
            {
                var result = await _handler.GetAllProductAsync();
                stopwatch.Stop();
                Console.WriteLine($"Tốn {stopwatch.ElapsedMilliseconds} ms");
                var response = new MessageRespondDTO<object>(
                    data: result,
                    status: true,
                    message: "Lấy sản phẩm thành công."
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"Tốn {stopwatch.ElapsedMilliseconds} ms: {ex.Message}");
                var errorResponse = new MessageRespondDTO<object>(
                    data: null,
                    status: false,
                    message: $"Đã xảy ra lỗi: {ex.Message}"
                );

                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            try
            {
                var result = await _handler.GetProductByIdM(id);

                var response = new MessageRespondDTO<object>(
                    data: result,
                    status: true,
                    message: "Lấy sản phẩm thành công."
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProductController(string id, UpdateProductDTO dataupdate)
        {
            try
            {
                var result = await _update_handler.UpdateHandler(id, dataupdate);

                var response = new MessageRespondDTO<object>(
                    data: dataupdate,
                    status: true,
                    message: "Update thành công"
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var result = await _delete_handler.DeleteHandler(id);

                var response = new MessageRespondDTO<object>(
                    data: id,
                    status: true,
                    message: "Xóa sản phẩm " + id+ " thành công." 
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
