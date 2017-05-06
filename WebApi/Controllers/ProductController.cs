using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return new Product[] {

                new Product { ProductID ="101", ProductName = "Samsung Galaxy", Category="Mobile"},
                new Product { ProductID ="102", ProductName = "Apple iPhone", Category="Mobile"},
                new Product { ProductID ="103", ProductName = "Apple TV", Category="Television"},
                new Product { ProductID ="104", ProductName = "Apple iPad", Category="Tablet"},
                new Product { ProductID ="105", ProductName = "MSFT Surface", Category="Tablet"},
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
