using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _products;

        public ProductsController(IProductRepository products)
        {
            _products = products;
        }

        // GET: api/Products
        [HttpGet]

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _products.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var products = await _products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                if (id != product.ProductId)
                {
                    return BadRequest();
                }



                try
                {
                    await _products.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _products.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducts(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product productNew = await _products.Add(product);

                    return CreatedAtAction("GetProducts", new { id = productNew.ProductId }, productNew);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return _products.Remove(id).Result;

        }

        private async Task<bool> ProductsExists(int id)
        {
            return await _products.Exists(id);
        }

        [HttpPost]
        [Route("UploadProductImage")]
        public async Task<ActionResult> UploadProductImage(IFormFile file)
        {
            try
            {
                
                //var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "ProductImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
