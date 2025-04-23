using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController(ICartService cartService):BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShopingCart>>GetCartById(string id)
        {
            ShopingCart? shopingCart = await cartService.GetCartAsync(id);
            return Ok(shopingCart ?? new ShopingCart{Id = id});
        }

        [HttpPost]

        public async Task<ActionResult<ShopingCart>> UpdateCart(ShopingCart cart)
        {
            ShopingCart? result = await cartService.SetCartAsync(cart);
            if(result== null)
            {
                return BadRequest("Problem with cart");
            }
            return result;
        }

        [HttpDelete]
        
        public async Task<ActionResult>DeleteCart(string id)
        {
            var result = await cartService.DeleteCartAsync(id);
            if(!result) return BadRequest("problem on deleting cart");
            return Ok();
        }  
    }
    
}