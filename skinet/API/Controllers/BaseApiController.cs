using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController():ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> genericRepository, ISpecification<T> spec,
        int pageIndex, int PageSize) where T: BaseEntity
        {
            var items = await genericRepository.ListAsync(spec);
            var count = await genericRepository.CountAsync(spec);
            var paggination = new Pagination<T>
            (pageIndex, PageSize, count, items);
            return Ok(paggination);

        }

        
    }
    
}