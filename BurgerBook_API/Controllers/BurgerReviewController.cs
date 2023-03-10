using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerBook_API.Models;
using BurgerBook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BurgerBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerReviewController : ControllerBase
    {
        private readonly BurgerReviewService _burgerReviewService;

        public BurgerReviewController(BurgerReviewService burgerReviewService)
        {
            _burgerReviewService = burgerReviewService;
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
        public async Task<ActionResult<List<BurgerReview>>> GetByBurgerPlaceId(string burgerPlaceId)
        {
            var burgerReview = await this._burgerReviewService.GetByBurgerPlaceIdAsync(burgerPlaceId);

            if (burgerReview == null)
            {
                return NotFound();
            }

            return burgerReview;
        }

        [HttpPost("{burgerPlaceId:length(24)}")]
        public async Task<IActionResult> Add(string burgerPlaceId)
        {
            var newBurgerReview = new BurgerReview() { BurgerPlaceId = burgerPlaceId };

            await this._burgerReviewService.CreateAsync(newBurgerReview);

            return CreatedAtAction(nameof(Get), new { id = newBurgerReview.Id }, newBurgerReview);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, BurgerReview updatedBurgerReview)
        {
            var burgerReview = await _burgerReviewService.GetAsync(id);

            if (burgerReview is null)
            {
                return NotFound();
            }

            updatedBurgerReview.Id = burgerReview.Id;

            await _burgerReviewService.UpdateAsync(id, updatedBurgerReview);

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
