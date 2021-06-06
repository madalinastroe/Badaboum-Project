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

    public class ShopsController : ControllerBase
    {
        private readonly ShopInterface _shopResource;

        public ShopsController(ShopInterface shopResource)
        {
            _shopResource = shopResource;
        }


        [HttpGet]
        [Route("api/shops")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Shop> shops = await _shopResource.GetAll();
            //return Ok(shops);

            return Ok(shops.Select(shop => new ShopViewModel()
            {
                shopID = shop.shopID,
                name = shop.name,
                
                sells = shop.sells.Select(sell => new ShopProductViewModel()
                {
                    shopID = sell.shopID,
                    shopName = sell.shop.name,
                    productID = sell.productID,
                    productName = sell.product.name,
                    quantity = sell.quantity,
                    stock = sell.stock,
                    price = sell.price,
                }),         



            }));

        }


        [HttpGet]
        [Route("api/shops/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Shop shop = await _shopResource.Get(id);

            if (shop == null)
            {
                return NotFound("Shop could not be found");
            }
            return Ok(shop);
        }



        [HttpPost]
        [Route("api/shops")]
        public async Task<IActionResult> Post([FromBody] Shop shop)
        {

            if (shop == null)
            {
                return BadRequest("Null shop");
            }

            await _shopResource.Add(shop);

            return Ok();
        }



        [HttpPut]
        [Route("api/shops/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Shop shop)
        {

            if (shop == null)
            {
                return BadRequest("Null shop");
            }

            Shop aux = await _shopResource.Get(id);

            if (aux == null)
            {
                return NotFound("Shop could not be found");
            }

            aux.shopID = shop.shopID;
            aux.name = shop.name;

            await _shopResource.Update(aux);

            return Ok();
        }

        [HttpDelete]
        [Route("api/shops/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            Shop shop = await _shopResource.Get(id);

            if (shop == null)
            {
                return NotFound("Shop could not be found");
            }

            await _shopResource.Delete(shop);
            return Ok();

        }


        [HttpPost]
        [Route("api/shops/getid")]
        public async Task<IActionResult> GetID(string shopName)
        {

            Shop aux = await _shopResource.GetID(shopName);

            if (aux == null)
            {
                return BadRequest();
            }

            return Ok(aux);


        }


    }
}
