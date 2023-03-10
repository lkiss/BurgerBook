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
    public class BurgerReviewController : ControllerBase
    {
        [HttpGet("getreviews/{placeId}")]
        public string GetReviews(string placeId)
        {
            return JsonConvert.SerializeObject(new { placeId = placeId });
        }

        [HttpGet("getreview/{placeId}/{reviewId}")]
        public string GetReviews(string placeId, string reviewId)
        {
            return JsonConvert.SerializeObject(new { placeId = placeId, reviewId = reviewId });
        }

        [HttpPost("addreview/{placeId}/{reviewId}")]
        public string AddReview(string placeId, string reviewId)
        {
            return JsonConvert.SerializeObject(new { placeId = placeId, reviewId = reviewId });
        }

    }
}
