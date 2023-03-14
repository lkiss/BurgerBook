using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerBook_API.Models;
using BurgerBook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MongoDB.Bson;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Reflection.Metadata;
using Azure.Storage;
using BurgerBook.Models.Constants;

namespace BurgerBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerReviewController : ControllerBase
    {
        private readonly BurgerReviewService _burgerReviewService;
        private readonly BlobServiceClient _blobServiceClient;

        public BurgerReviewController(BurgerReviewService burgerReviewService, BlobServiceClient blobServiceClient)
        {
            _burgerReviewService = burgerReviewService;
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet]
        public async Task<List<BurgerReview>> Get() => await _burgerReviewService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<BurgerReview>> Get(string id)
        {
            var burgerReview = await this._burgerReviewService.GetAsync(id);

            if (burgerReview == null)
            {
                return NotFound();
            }

            return burgerReview;
        }

        [HttpGet("getbyburgerplaceid/{burgerPlaceId:length(24)}")]
        public async Task<ActionResult<List<BurgerReview>>> GetByBurgerPlaceId(string? burgerPlaceId)
        {
            if (string.IsNullOrEmpty(burgerPlaceId))
            {
                return new List<BurgerReview>();
            }

            var burgerReview = await this._burgerReviewService.GetByBurgerPlaceIdAsync(burgerPlaceId);

            if (burgerReview == null)
            {
                return NotFound();
            }

            return burgerReview;
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] BurgerReview newBurgerReview)
        {
            newBurgerReview.Id = ObjectId.GenerateNewId().ToString();
            newBurgerReview.PictureUrl = AzureConstants.CDN_URL
                + (!string.IsNullOrEmpty(newBurgerReview.PictureUrl) && newBurgerReview.PictureUrl.Equals("true")
                    ? newBurgerReview.Id + ".jpeg"
                    : "default.jpg");

            await this._burgerReviewService.CreateAsync(newBurgerReview);

            return CreatedAtAction(nameof(Get), new { id = newBurgerReview.Id }, newBurgerReview);
        }

        [HttpPost("uploadreviewimage")]
        public async Task<IActionResult> UploadReviewImage([FromQuery] string reviewId, [FromQuery] string placeId, IFormFile file)
        {
            if (string.IsNullOrEmpty(placeId) && string.IsNullOrEmpty(reviewId))
            {
                return BadRequest("placeId and reviewId must contain a value");
            };

            long size = file.Length;
            var fileName = reviewId + ".jpeg";

            if (file.Length > 0 && file.Length < 2048000)
            {
                using (var stream = file.OpenReadStream())
                {
                    var blobContainerClient = _blobServiceClient.GetBlobContainerClient("burgerbookimages");
                    var blobClient = blobContainerClient.GetBlobClient(fileName);

                    var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
                    var uploadedBlob = await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
                }
            }
            else
            {
                return BadRequest(new { error = "Empty file or file bigger than 2MB is not allowed" });
            }

            return Ok(new { size, reviewId, placeId });
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] BurgerReview updatedBurgerReview)
        {
            var burgerReview = await _burgerReviewService.GetAsync(updatedBurgerReview.Id);

            if (burgerReview is null)
            {
                return NotFound();
            }

            updatedBurgerReview.BurgerPlaceId = burgerReview.BurgerPlaceId;

            await _burgerReviewService.UpdateAsync(updatedBurgerReview.Id, updatedBurgerReview);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var burgerReview = await _burgerReviewService.GetAsync(id);

            if (burgerReview is null)
            {
                return NotFound();
            }

            await _burgerReviewService.RemoveAsync(id);

            return NoContent();
        }

    }
}
