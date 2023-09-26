using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7132/api/products";
            CategoryApiUrl = "https://localhost:7132/api/categories";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl);
            List<Product>? listProducts = new List<Product>();
            if(productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listProducts = await productResponse.Content.ReadFromJsonAsync<List<Product>>();
            }

            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }

            ViewData["CategoryId"] = new SelectList(listCategories, "CategoryId", "CategoryName");

            return View(listProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
            Product? product = new Product();
            if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = await productResponse.Content.ReadFromJsonAsync<Product>();
            }

            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }

            ViewData["category"] = listCategories;

            return View(product);
        }

        public async Task<ActionResult> Create()
        {
            HttpResponseMessage cateResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (cateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = await cateResponse.Content.ReadFromJsonAsync<List<Category>>();
            }

            ViewData["category"] = listCategories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product p)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, p);
            if (ModelState.IsValid)
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return Redirect("Create");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage productResponse = await client.GetAsync(ProductApiUrl + $"/{id}");
            Product product = new Product();
            if (productResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = productResponse.Content.ReadFromJsonAsync<Product>().Result;
            }

            HttpResponseMessage categoryResponse = await client.GetAsync(CategoryApiUrl);
            List<Category>? listCategories = new List<Category>();
            if (categoryResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listCategories = categoryResponse.Content.ReadFromJsonAsync<List<Category>>().Result;
            }
            ViewData["category"] = listCategories;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product p)
        {
            if (ModelState.IsValid)
            {
                await client.PutAsJsonAsync(ProductApiUrl + $"/{p.ProductId}", p);
                return RedirectToAction("Index");
            }

            return View(p);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await client.DeleteAsync(ProductApiUrl + $"/{id}");
            return RedirectToAction("Index");
        }
    }
}