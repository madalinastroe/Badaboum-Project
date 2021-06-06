using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{

    public class ShopProductsController : ControllerBase
    {
        private readonly ShopProductsInterface _shopProductsResource;
        public ShopProductsController(ShopProductsInterface shopProductsResource)
        {
            _shopProductsResource = shopProductsResource;
        }

        [HttpGet]
        [Route("api/shopproducts")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ShopProducts> shopProducts = await _shopProductsResource.GetAll();
            return Ok(shopProducts.Select(shopprod => new ShopProductViewModel()
            {
                shopID = shopprod.shopID,
                shopName = shopprod.shop.name,
                productID = shopprod.productID,
                productName = shopprod.product.name,
                productPicture = shopprod.product.picture,
                quantity = shopprod.quantity,
                stock = shopprod.stock,
                price = shopprod.price,

            }));
        }


        [HttpGet]
        [Route("api/shopproducts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ShopProducts shopProducts = await _shopProductsResource.Get(id);

            if (shopProducts == null)
            {
                return NotFound("Shop could not be found");
            }
            return Ok(shopProducts);
        }



        [HttpPost]
        [Route("api/shopproducts")]
        public async Task<IActionResult> Post([FromBody] ShopProducts shop)
        {

            if (shop == null)
            {
                return BadRequest("Null shop");
            }

            await _shopProductsResource.Add(shop);

            return Ok();
        }



        [HttpPut]
        [Route("api/shopproducts/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ShopProducts shop)
        {

            if (shop == null)
            {
                return BadRequest("Null shop");
            }

            ShopProducts aux = await _shopProductsResource.Get(id);

            if (aux == null)
            {
                return NotFound("Shop could not be found");
            }

            aux.shopID = shop.shopID;
            aux.productID = shop.productID;
            aux.quantity = shop.quantity;
            aux.stock = shop.stock;
            aux.price = shop.price;

            await _shopProductsResource.Update(aux);

            return Ok();
        }

        [HttpDelete]
        [Route("api/shopproducts/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            ShopProducts shop = await _shopProductsResource.Get(id);

            if (shop == null)
            {
                return NotFound("Shop could not be found");
            }

            await _shopProductsResource.Delete(shop);
            return Ok();

        }

        //=================================================================
        [HttpPost]
        [Route("api/shopproducts/getSP")]
        public async Task<IActionResult> GetByNames([FromBody] ShopProducts sp)
        {
            ShopProducts aux = await _shopProductsResource.getByShopAndProduct(sp.shopID, sp.productID);


            if (aux == null)
            {
                return NotFound();
            }

            return Ok(aux);
        }

        //====================================================================
        [HttpPut]
        [Route("api/shopproducts/updateElement")]
        public async Task<IActionResult> UpdateElement([FromBody] ShopProducts sp)
        {
            ShopProducts aux = await _shopProductsResource.getByShopAndProduct(sp.shopID, sp.productID);

            if (sp == null)
            {
                return BadRequest("Null shop");
            }

            if (aux == null)
            {
                return NotFound("Shop could not be found");
            }

            aux.shopID = sp.shopID;
            aux.productID = sp.productID;
            aux.quantity = sp.quantity;
            aux.stock = sp.stock;
            aux.price = sp.price;

            await _shopProductsResource.Update(aux);

            return Ok();
        }

        [HttpDelete]
        [Route("api/shopproducts/deletesp")]

        public async Task<IActionResult> DeleteSP(int shopID, int productID)
        {
            ShopProducts aux = await _shopProductsResource.getByShopAndProduct(shopID, productID);

            if (aux == null)
            {
                return NotFound("Shop could not be found");
            }

            await _shopProductsResource.Delete(aux);
            return Ok();

        }
    }
}
