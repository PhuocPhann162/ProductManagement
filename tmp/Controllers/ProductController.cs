using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace tmp.Controllers
{
	public class ProductController : Controller
	{
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7132/api/ProductsControllers";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
                foreach (Product p in listProducts)
                {
                    if (p.ProductId == id)
                    {
                        return View(p);
                    }
                }
                return NotFound();
            }
            else return NotFound();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product p)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(p), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                }
            }
            return View(p);
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Product product = JsonSerializer.Deserialize<Product>(strData, options);
                return View(product);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
