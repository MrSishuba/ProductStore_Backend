using Assignment3_Backend.Models;
using Assignment3_Backend.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Assignment3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IRepository _repository;
        public StoreController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("ProductListing")]
        public async Task<ActionResult> ProductListing()
        {
            try
            {
                var results = await _repository.GetProductsAsync();

                dynamic products = results.Select(product => new
                {
                    product.ProductId,
                    product.Price,
                    ProductTypeName = product.ProductType.Name,
                    BrandName = product.Brand.Name,
                    product.Name,
                    product.Description,
                    product.DateCreated,
                    product.DateModified,
                    product.IsActive,
                    product.IsDeleted,
                    product.Image
                });

                return Ok(products);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error: Could not find the selected product");
            }
        }

        [HttpPost] 
        [DisableRequestSizeLimit]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] IFormCollection formData)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();

                var file = formCollection.Files.First();

                if (file.Length > 0)
                {

                    using (var memroryObject = new MemoryStream())
                    {
                        file.CopyTo(memroryObject);
                        var fileBytes = memroryObject.ToArray();
                        string base64 = Convert.ToBase64String(fileBytes);

                        string price = formData["price"];
                        decimal num = decimal.Parse(price.Replace(".", ","));

                        var product = new Product
                        {
                            Price = num
                            ,
                            Name = formData["name"]
                            ,
                            Description = formData["description"]
                            ,
                            BrandId = Convert.ToInt32(formData["brand"])
                            ,
                            ProductTypeId = Convert.ToInt32(formData["producttype"])
                            ,
                            Image = base64
                            ,
                            DateCreated = DateTime.Now
                        };


                        _repository.Add(product);
                        await _repository.SaveChangesAsync();
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex}");
            }
        }


        [HttpGet]
        [Route("Brands")]
        public async Task<ActionResult> Brands()
        {
            try
            {
                var results = await _repository.GetBrandsAsync();

                return Ok(results);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error:Could not find the seletcted brands please refresh the page and try again optionally contact the help");
            }
        }


        [HttpGet]
        [Route("ProductTypes")]
        public async Task<ActionResult> ProductTypes()
        {
            try
            {
                var results = await _repository.GetProductTypesAsync();

                return Ok(results);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error:Could not find the seletcted product type please refresh the page and try again optionally contact the help");
            }
        }

    }
}