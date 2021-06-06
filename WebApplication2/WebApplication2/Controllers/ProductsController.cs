using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{

    public class ProductsController : ControllerBase
    {
        private readonly Context _context;
        private readonly ProductInterface _productResource;


        public ProductsController(Context context, ProductInterface productResource)
        {
            _context = context;
            _productResource = productResource;
        }



        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Product> products = await _productResource.GetAll();
            return Ok(products.Select(product => new ProductViewModel()
            {
                productID = product.productID,
                name = product.name,
                description = product.description,
                picture = product.picture,

                ratings = product.ratings.Select(rating => new RatingViewModel()
                {
                    productID = rating.productID,
                    productName = rating.product.name,
                    userID = rating.userID,
                    userName = rating.user.firstName,
                    RatingValue = rating.RatingValue
                }),

                
                purchasedProducts = product.purchasedProducts.Select(purchase => new ShopProductViewModel()
                {
                    shopID = purchase.shopID,
                    shopName = purchase.shop.name,
                    productID = purchase.productID,
                    productPicture = purchase.product.picture,
                    productName=purchase.product.name,
                    quantity = purchase.quantity,
                    stock = purchase.stock,
                    price = purchase.price
                }),

                
                

            }));
        }

        [HttpGet]
        [Route("api/products/{id}")]

        public async Task<IActionResult> Get(int id)
        {
            Product product = await _productResource.Get(id);

            if (product == null)
            {
                return NotFound("Product could not be found");
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("api/products/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {

            if (product == null)
            {
                return BadRequest("Null product");
            }

            Product aux = await _productResource.Get(id);

            if (aux == null)
            {
                return NotFound("Product could not be found");
            }

            aux.productID = product.productID;
            aux.name = product.name;
            aux.description = product.description;
            aux.picture = product.picture;

            await _productResource.Update(aux);

            return Ok();
        }

        [HttpPost]
        [Route("api/products")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Null product");
            }

            await _productResource.Add(product);

            return Ok();
        }


        [HttpDelete]
        [Route("api/products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productResource.Get(id);

            if (product == null)
            {
                return NotFound("Product could not be found");
            }

            await _productResource.Delete(product);
            return Ok();

        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productID == id);
        }


        //===========================================================================================
        [HttpPost]
        [Route("api/products/getpicture")]
        public async Task<IActionResult> GetProductPicture(string productName)
        {

            Product aux = await _productResource.GetPicture(productName);

            if (aux == null)
            {
                return BadRequest();
            }

            return Ok(aux);

           
        }

    }
}
