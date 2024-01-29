using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.DeleteProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.Queries.GetProducts;
using ProductManagement.WebApi.ActionFilters;
using System.Collections.Generic;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("createProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create(CreateProductCommandRequest createProductCommandRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            createProductCommandRequest.CreatorId = userId.ToString();

            CreateProductCommandResponse result = await _mediator.Send(createProductCommandRequest);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("updateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                updateProductCommandRequest.CurrentUserId = userId.ToString();

                var result = await _mediator.Send(updateProductCommandRequest);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message); // Forbidden status code
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                DeleteProductCommandRequest deleteProductCommandRequest = new()
                {
                    Id = id,
                    CurrentUserId = userId.ToString()
                };

                var deletedProductId = await _mediator.Send(deleteProductCommandRequest);

                return Ok(deletedProductId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name=null)
        {
            GetProductsQueryRequest request = new();
            request.Name = name;

            List<GetProductsQueryResponse> result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
