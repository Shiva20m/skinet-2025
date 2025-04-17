using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// namespace API.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProductsController: ControllerBase
//     {
//         private readonly StoreContext _storeContext;

//         public ProductsController(StoreContext storeContext)
//         {
//             _storeContext=storeContext;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
//         {
//             IEnumerable<Product> products = await _storeContext.Products.ToListAsync();
//             return Ok(products);
//         }
//         [HttpGet("{id:int}")]
//         public async Task<ActionResult<Product>>GetProduct(int id)
//         {
//             Product? product = await _storeContext.Products.FindAsync(id);
//             if(product==null)
//             {
//                 return NotFound();
//             }
//             return product;
//         }
//         [HttpPost]
//         public async Task<ActionResult<Product>>CreateProduct([FromBody]Product product)
//         {
//             _storeContext.Products.Add(product);
//             await _storeContext.SaveChangesAsync();
//             return product;
//         }

//         [HttpPut("{id:int}")]
//         public async Task<ActionResult>UpdateProduct(int id, Product product)
//         {
//             if(ProductExists(id))
//             {
//                 _storeContext.Entry(product).State = EntityState.Modified;
//                 await _storeContext.SaveChangesAsync();
//                 return Ok("Updated SucessFully.");
//             }
//             return BadRequest("Cannot update the table");
//         }
//         private Boolean ProductExists(int id)
//         {
//             // lamda function-for each x is prouct if there is any x whose x.id == x.id
//             return _storeContext.Products.Any(x=> x.Id==id);

//         }
//         [HttpDelete("{id:int}")]
//         public async Task<IActionResult> DeleteProduct(int id)
//         {
//             Product? product = await _storeContext.Products.FindAsync(id);
//             if(product!= null)
//             {
//                 _storeContext.Products.Remove(product);
//                 await _storeContext.SaveChangesAsync();
//                 return Ok("Deleted sucessfully");

//             }
//             return NotFound();
//         }

//     }  
// }
// implement using the Iuserinterface

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsWithoutGenericController(IProductRepository productRepository): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            IEnumerable<Product> products = await productRepository.GetProductsAsync(brand, type, sort);
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>>GetProduct(int id)
        {
            Product? product = await productRepository.GetProductByIdAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>>CreateProduct([FromBody]Product product)
        {
            productRepository.AddProduct(product);
            bool result = await productRepository.SaveChangesAsync();
            if(result)
            {
                return Ok("Created Sucessfully");
                // return CreatedAtAction("GetProduct", new {id = product.Id}, product);

            }
            return BadRequest("Problem In Creating The Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>UpdateProduct(int id, Product product)
        {
            if(ProductExists(id))
            {
                productRepository.UpdateProduct(product);
                bool result = await productRepository.SaveChangesAsync();
                if(result)
                {
                    return Ok("Updated SucessFully.");
                }
            }
            return BadRequest("Cannot update the table");
        }
        private Boolean ProductExists(int id)
        {
            // lamda function-for each x is prouct if there is any x whose x.id == x.id
            return productRepository.ProductExists(id);

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product? product = await productRepository.GetProductByIdAsync(id);
            if(product!= null)
            {
                productRepository.DeleteProduct(product);
                bool result = await productRepository.SaveChangesAsync();
                if(result)
                {
                    return Ok("Deleted sucessfully");
                }
                
            }
            return BadRequest("Problem In Deleting The Product");
        }
        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<string>>>GetBrands()
        {
           IEnumerable<string> result =  await productRepository.GetBrandAsync();
           return Ok(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>>GetTypes()
        {
            IEnumerable<string> result = await productRepository.GetTypesAsyn();
            return Ok(result);
        }

    }
    
}