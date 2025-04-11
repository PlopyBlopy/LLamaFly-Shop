using API.Contracts.Requests;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/images/products")]
    public class ProductsImagesController : ControllerBase
    {
        private readonly IImageService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<ImageFormRequest> _validator;
        private readonly IFileConverter _fileConverter;

        public ProductsImagesController(IImageService service, IMapper mapper, IValidator<ImageFormRequest> validator, IFileConverter fileConverter) //
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
            _fileConverter = fileConverter;
        }

        [Authorize(Policy = "seller")]
        [HttpPost("upload")]
        public async Task<ActionResult> AddProductImages([FromForm] ImageUploadRequest request, CancellationToken ct)
        {
            //var validationResult = await _validator.ValidateAsync(request.Images[0], ct);

            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            //}

            //TODO: При добавлении фото продукта, обновить cache (удалить)

            ImageUploadDto dto = _mapper.Map<ImageUploadDto>(request);

            await _service.Add(dto, ct);

            return Created();
        }

        //[HttpGet("{productId}/preview")]
        //public async Task<ActionResult> GetProductPreviewImage([FromRoute] Guid productId, CancellationToken ct)
        //{
        //    var result = await _service.GetProductPreviewImage(productId, ct);

        //    var acceptHeader = Request.Headers["Accept"].ToString();
        //    bool supportsWebP = acceptHeader.Contains("image/webp");

        //    if (supportsWebP && result.MimeType != "image/webp")
        //    {
        //        try
        //        {
        //            using var imageStream = new MemoryStream(result.Data);
        //            var webpData = await _fileConverter.ConvertToWebP(imageStream, 60, ct);
        //            return File(webpData, "image/webp", $"{result.Order}.webp");
        //        }
        //        catch
        //        {
        //            return File(result.Data, result.MimeType, $"{result.Order}");
        //        }
        //    }

        //    return File(result.Data, result.MimeType, $"{result.Order}");
        //}

        [HttpPost("preview/range")]
        public async Task<ActionResult> GetProductsPreviewImages([FromBody] ProductIdsRequest request, CancellationToken ct)
        {
            var acceptHeader = Request.Headers.Accept.ToString();
            bool supportsWebP = acceptHeader.Contains("image/webp");

            var zipBytes = await _service.GetProductsPreviewImages(
                request.ProductIds,
                supportsWebP,
                ct);

            Response.Headers.Append("X-Archive-Hash", ComputeProductIdsHash(request.ProductIds));
            return File(zipBytes, "application/zip", $"previews_{DateTime.Now:yyyyMMddHHmm}.zip");
        }

        [HttpGet("detail/{productId}")]
        public async Task<ActionResult> GetProductImages([FromRoute] Guid productId, CancellationToken ct)// Использование [FromQuery]
        {
            var acceptHeader = Request.Headers.Accept.ToString();
            bool supportsWebP = acceptHeader.Contains("image/webp");

            var zipBytes = await _service.GetProductImages(productId, supportsWebP, ct);

            return File(zipBytes, "application/zip", $"{productId}.zip");
        }

        [Authorize(Policy = "seller")]
        [HttpPatch]
        public async Task<ActionResult> Update([FromForm] ImageUploadRequest request, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(request.Images[0], ct);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            ImageUploadDto dto = _mapper.Map<ImageUploadDto>(request);

            await _service.Update(dto, ct);
            return Ok();
        }

        [Authorize(Policy = "seller")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProductImages([FromQuery] Guid productId, CancellationToken ct)
        {
            return Ok();
        }

        private string ComputeProductIdsHash(Guid[] productIds)
        {
            var orderedIds = productIds.OrderBy(x => x).Select(x => x.ToString());
            var idsString = string.Join("|", orderedIds);

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(idsString));
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}