using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerBook.Services;
using BurgerBook_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BurgerBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerPlaceController : ControllerBase
    {
        private readonly BurgerPlaceService _burgerPlaceService;
        private readonly BurgerReviewService _burgerReviewService;

        public BurgerPlaceController(BurgerPlaceService burgerPlaceService, BurgerReviewService burgerReviewService)
        {
            _burgerPlaceService = burgerPlaceService;
            _burgerReviewService = burgerReviewService;
        }

        [HttpGet]
        public async Task<List<BurgerPlace>> Get() => await _burgerPlaceService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<BurgerPlace>> Get(string id)
        {
            var burgerPlace = await this._burgerPlaceService.GetAsync(id);

            if(burgerPlace == null)
            {
                return NotFound();
            }

            return burgerPlace;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BurgerPlace newBurgerPlace)
        {
            newBurgerPlace.Id = ObjectId.GenerateNewId().ToString();

            await this._burgerPlaceService.CreateAsync(newBurgerPlace);

            return CreatedAtAction(nameof(Get), new { id = newBurgerPlace.Id }, newBurgerPlace );
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, BurgerPlace updatedBurgerPlace)
        {
            var originalBurgerPlace = await _burgerPlaceService.GetAsync(id);

            if (originalBurgerPlace is null)
            {
                return NotFound();
            }

            updatedBurgerPlace.Id = originalBurgerPlace.Id;

            await _burgerPlaceService.UpdateAsync(id, updatedBurgerPlace);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var burgerPlace = await _burgerPlaceService.GetAsync(id);

            if (burgerPlace is null)
            {
                return NotFound();
            }

            await _burgerReviewService.RemoveByBurgerPlaceIdAsync(id);
            await _burgerPlaceService.RemoveAsync(id);

            return NoContent();
        }

    }
}
