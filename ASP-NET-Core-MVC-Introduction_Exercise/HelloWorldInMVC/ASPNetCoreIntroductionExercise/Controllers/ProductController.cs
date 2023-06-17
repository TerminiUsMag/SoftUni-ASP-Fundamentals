using ASPNetCoreIntroductionExercise.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ASPNetCoreIntroductionExercise.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> _products
            = new List<ProductViewModel>()
            {
             new ProductViewModel()
             {
                 Id=1,
                 Name="Cheese",
                 Price = 7.00
             },
             new ProductViewModel()
             {
                 Id=2,
                 Name = "Ham",
                 Price = 5.50
             },
             new ProductViewModel()
             {
                 Id=3,
                 Name = "Bread",
                 Price = 1.50
             }
            };
        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword is not null)
            {
                var foundProducts = _products.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
                return View(foundProducts);
            }
            return View(_products);
        }

        public IActionResult ById(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product is null)
            {
                return BadRequest();
            }
            return View(product);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(_products, options);
        }

        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var product in _products)
            {
                text += $"Product {product.Id}: {product.Name} - {product.Price} lv.";
                text += "\r\n";
            }
            return Content(text);
        }

        public IActionResult AllAsTextFile()
        {
            var sb = new StringBuilder();
            foreach (var product in _products)
            {
                sb.AppendLine($"Product {product.Id}: {product.Name} - {product.Price:f2} lv.");
            }
            Response.Headers.Add(HeaderNames.ContentDisposition,
                @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}
