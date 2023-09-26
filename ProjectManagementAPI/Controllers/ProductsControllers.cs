using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        //GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        //GET: api/products/id
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProduct(int id) => repository.GetProductById(id);

        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult PutProduct(int id) 
        {
            var p = repository.GetProductById(id);
            if(p == null) 
                return NotFound();
            repository.UpdateProduct(p);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
                return NotFound();
            repository.DeleteProduct(p);
            return NoContent();
        }


        
    }
}
