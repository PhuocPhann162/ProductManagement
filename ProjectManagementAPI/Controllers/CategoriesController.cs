using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private IProductRepository repository = new ProductRepository();

		[HttpGet]
		public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();
	}
}
