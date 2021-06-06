using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class ControllerUser : ControllerBase
    {
        private readonly IGenericInterface _userResource;
        private readonly IConfiguration _configuration;

        public ControllerUser(IGenericInterface userResource, IConfiguration configuration)
        {
            _userResource = userResource;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<User> users = await _userResource.GetAll();


            return Ok(users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                firstName = user.firstName,
                lastName=user.lastName,
                email = user.email,
                address = user.address,
                password = user.password,
                avatar = user.avatar,
                role = "user",

                ratedProducts = user.ratedProducts.Select(rating => new RatingViewModel()
                {
                    productID = rating.productID,
                    productName = rating.product.name,
                    userID = rating.userID,
                    userName = rating.user.firstName,
                    RatingValue = rating.RatingValue
                }),


          }));

        }



        [HttpGet]
        [Route("api/users/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _userResource.Get(id);

            if(user==null)
            {
                return NotFound("User could not be found");
            }
            return Ok(user);
        }



        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            //vreau sa verific daca mai am alt user cu adresa asta de email
            User aux = await _userResource.GetByEmail(user.email);

           
            if(aux != null)
            { 
                return Conflict("Existing user");
            }

            await _userResource.Add(user);

            return Ok();
        }

       

        [HttpPut]
        [Route("api/users/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {

            if (user == null)
            {
                return BadRequest("Null user");
            }

            User aux = await _userResource.Get(id);

            if (aux == null)
            {
                return NotFound("User could not be found");
            }

            aux.firstName = user.firstName;
            aux.lastName = user.lastName;
            aux.email = user.email;
            aux.address = user.address;
            aux.avatar = user.avatar;
            aux.role = "user";                        

            await _userResource.Update(aux);

            return Ok();
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _userResource.Get(id);

            if (user == null)
            {
                return NotFound("User could not be found");
            }

            await _userResource.Delete(user);
            return Ok();

        }

        //=================================================================================
        [HttpPost]
        [Route("api/users/login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User aux = await _userResource.GetByAccount(user.email, user.password);
            if (aux == null)
            {
                return NotFound();
            }

           
            return Ok(aux);
        }

    }
}
