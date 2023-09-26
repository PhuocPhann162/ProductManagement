using BusinessObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementWebClient.Models
{
	public class CategoryViewModel
	{
		public int CategoryId { get; set; }	
		public string CategoryName { get; set; }
		public List<SelectListItem> Categories { get; } = new List<SelectListItem>();
		
		public CategoryViewModel(IEnumerable<Category> categories)
		{
			foreach(var item in categories)
			{
				Categories.Add(new SelectListItem
				{
					Value = item.CategoryId.ToString(),
					Text = item.CategoryName
				});
			}
		}
	}
}
