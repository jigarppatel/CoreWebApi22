using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebApi22.Contracts;
using CoreWebApi22.Models;

namespace CoreWebApi22.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategories;

        public ProductCategoriesController(IProductCategoryRepository productCategories)
        {
            _productCategories = productCategories;
        }

        // GET: api/Products
        [HttpGet]

        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            return await _productCategories.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategories(int id)
        {
            var products = await _productCategories.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategories(int id, ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                if (id != productCategory.ProductCategoryId)
                {
                    return BadRequest();
                }



                try
                {
                    await _productCategories.Update(productCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productCategories.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategories(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                ProductCategory productCatNew = await _productCategories.Add(productCategory);

                return CreatedAtAction("GetProductCategories", new { id = productCatNew.ProductCategoryId }, productCatNew);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int id)
        {

            var product = await _productCategories.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return _productCategories.Remove(id).Result;

        }

        private async Task<bool> ProductCategoriesExists(int id)
        {
            return await _productCategories.Exists(id);
        }
    }
}
