using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BurgerBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerPlaceController : ControllerBase
    {

        [HttpGet("getplaces")]
        public string GetBurgerPlaces()
        {
            return JsonConvert.SerializeObject(new { placeId = 1 });
        }

        [HttpGet("getplace/{placeId}")]
        public string GetBurgerPlace(string placeId)
        {
            return JsonConvert.SerializeObject(new { placeId = placeId });
        }
    }
}
