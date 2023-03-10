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

        public BurgerPlaceController(BurgerPlaceService burgerPlaceService)
        {
            _burgerPlaceService = burgerPlaceService;
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
        public async Task<IActionResult> Add()
        {
            var newBurgerPlace = new BurgerPlace() { Name = "BestBurgers" };

            await this._burgerPlaceService.CreateAsync(newBurgerPlace);

            return CreatedAtAction(nameof(Get), new { id = newBurgerPlace.Id }, newBurgerPlace );
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, BurgerPlace updatedBurgerPlace)
        {
            var burgerPlace = await _burgerPlaceService.GetAsync(id);

            if (burgerPlace is null)
            {
                return NotFound();
            }

            updatedBurgerPlace.Id = burgerPlace.Id;

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

            await _burgerPlaceService.RemoveAsync(id);

            return NoContent();
        }

    }
}
