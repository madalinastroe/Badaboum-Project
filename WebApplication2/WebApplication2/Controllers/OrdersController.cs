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
    public class OrdersController : ControllerBase
    {
        private readonly OrderInterface _orderResource;

        public OrdersController(OrderInterface orderResource)
        {
            _orderResource = orderResource;
        }

        [HttpGet]
        [Route("api/orders/user/{id}")]
        public async Task<IActionResult> GetUserOrders(int id)
        {
            IEnumerable<Order> orders = await _orderResource.GetAll(id);
            // return Ok(orders);

            return Ok(orders.Select(order => new OrderViewModel()
            {
                orderID = order.orderID,
                shopID = order.shopID,
                shopName = order.shop.name,
                productID = order.productID,
                productName = order.product.name,
                userID = order.userID,
                userName = order.user.firstName,
                status = order.status,
                price = order.price,
            }));
        }


        [HttpGet]
        [Route("api/orders/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Order order = await _orderResource.Get(id);

            if (order == null)
            {
                return NotFound("Order could not be found");
            }
            return Ok(order);
        }



        [HttpPost]
        [Route("api/orders")]
        public async Task<IActionResult> Post([FromBody] Order order)
        {

            if (order == null)
            {
                return BadRequest("Null order");
            }

            await _orderResource.Add(order);

            return Ok();
        }



        [HttpPut]
        [Route("api/orders/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {

            if (order == null)
            {
                return BadRequest("Null order");
            }

            Order aux = await _orderResource.Get(id);

            if (aux == null)
            {
                return NotFound("Order could not be found");
            }

            aux.orderID = order.orderID;
            aux.shopID = order.shopID;
            aux.productID = order.productID;
            aux.userID = order.userID;
            aux.status = order.status;
            aux.price = order.price;

            await _orderResource.Update(aux);

            return Ok();
        }

        [HttpDelete]
        [Route("api/orders/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            Order order = await _orderResource.Get(id);

            if (order == null)
            {
                return NotFound("Order could not be found");
            }

            await _orderResource.Delete(order);
            return Ok();

        }


    }
}
