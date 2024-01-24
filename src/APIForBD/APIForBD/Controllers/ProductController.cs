using APIForBD.Contracts.Product;
using APIForBD.Contracts.User;
using APIForBD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIForBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public praktikaContext Context { get; }

        public ProductController(praktikaContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Вывод данных о всех продуктах
        /// </summary>
        /// /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() // Получение всех записей
        {
            List<Product> product = Context.Products.ToList();
            return Ok(product);
        }
        /// <summary>
        /// Вывод данных об одном конкретном продукте
        /// </summary>
        /// /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Получение одной записи
        {
            Product? product = Context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(product);
        }
        /// <summary>
        /// Создание нового продукта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "productName" : "яблоко",
        ///        "productDescription" : "вкусный фрукт",
        ///        "season" : "лето-осень",
        ///        "price" : "15.65",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductRequest request) // Создание одной записи
        {
            var productDto = request.Adapt<Product>();
            Context.Products.Add(productDto);
            Context.SaveChanges();
            return Ok(productDto);
        }
        /// <summary>
        /// Изменение текущих данных продукта
        /// </summary>
        /// /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "productName" : "Яблоко",
        ///        "productDescription" : "Вкусный фрукт",
        ///        "season" : "Лето-осень",
        ///        "price" : "15.65",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateProductRequest request)// Изменение существующей записи
        {
            var productDto = request.Adapt<Product>();
            Context.Products.Update(productDto);
            Context.SaveChanges();
            return Ok(productDto);
        }
        /// <summary>
        /// Удаление данных о выбранном продукте
        /// </summary>
        /// /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) // Удаление существующей записи
        {
            Product? product = Context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return BadRequest("Not Found");
            }
            Context.Products.Remove(product);
            Context.SaveChanges();
            return Ok(product);
        }
    }
}
