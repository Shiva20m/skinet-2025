using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> genericRepository): BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpecification(productSpecParams);
            var paggination = await CreatePagedResult(genericRepository, spec, productSpecParams.pageIndex, productSpecParams.PageSize);
            return Ok(paggination);
            
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>>GetProductById(int id)
        {
            Product? product = await genericRepository.GetByIdAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>>CreateProduct([FromBody]Product product)
        {
            genericRepository.Add(product);
            bool result = await genericRepository.SaveAllAsync();
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
                genericRepository.Update(product);
                bool result = await genericRepository.SaveAllAsync();
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
            return genericRepository.Exists(id);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product? product = await genericRepository.GetByIdAsync(id);
            if(product!= null)
            {
                genericRepository.Remove(product);
                bool result = await genericRepository.SaveAllAsync();
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
           var spec = new BrandListSpecification();
           IEnumerable<string> result =  await genericRepository.ListAsync<string>(spec);
           return Ok(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>>GetTypes()
        {
            var spec = new TypeListSpecification();
            IEnumerable<string> result = await genericRepository.ListAsync<string>(spec);
            return Ok(result);
        }

    }
    
}