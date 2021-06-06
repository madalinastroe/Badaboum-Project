using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public class RatingsController : ControllerBase
    {
        private readonly Context _context;
        private readonly RatingInterface _ratingResource;


        public RatingsController(Context context, RatingInterface ratingResource)
        {
            _context = context;
            _ratingResource = ratingResource;
        }



        [HttpGet]
        [Route("api/ratings")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Rating> ratings = await _ratingResource.GetAll();
            return Ok(ratings.Select(rating => new RatingViewModel()
            {
                productID = rating.productID,
                productName = rating.product.name,
                userID = rating.userID,
                userName = rating.user.firstName,
                RatingValue = rating.RatingValue
            }));


            //return Ok(ratings);

        }

        [HttpGet]
        [Route("api/ratings/{id}")]

        public async Task<IActionResult> Get(int id)
        {
            Rating rating = await _ratingResource.Get(id);

            if (rating == null)
            {
                return NotFound("Rating could not be found");
            }
            return Ok(rating);
        }

        [HttpPut]
        [Route("api/ratings/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rating rating)
        {

            if (rating == null)
            {
                return BadRequest("Null product");
            }

            Rating aux = await _ratingResource.Get(id);

            if (aux == null)
            {
                return NotFound("Rating could not be found");
            }

            aux.productID = rating.productID;
            aux.userID = rating.userID;
            aux.RatingValue = rating.RatingValue;

            await _ratingResource.Update(aux);

            return Ok();
        }

        [HttpPost]
        [Route("api/ratings")]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            if (rating == null)
            {
                return BadRequest("Null rating");
            }

            await _ratingResource.Add(rating);

            return Ok();
        }


        [HttpDelete]
        [Route("api/ratings/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Rating rating = await _ratingResource.Get(id);

            if (rating == null)
            {
                return NotFound("Rating could not be found");
            }

            await _ratingResource.Delete(rating);
            return Ok();

        }


    }
}
