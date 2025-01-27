using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repo) : BaseAPIController
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParam specParam)
    {
        var spec = new ProductSpecification(specParam);
       
        return await CreatePagedResult(repo, spec, specParam.PageIndex, specParam.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null ) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if(await repo.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new{id = product.Id}, product);
        }
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) 
            return BadRequest("Cannot update this product");

        repo.Update(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);   
        if (product == null) return NotFound();
        repo.Delete(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<String>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<String>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }

}
