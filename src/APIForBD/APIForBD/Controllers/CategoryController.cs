using APIForBD.Contracts.Category;
using APIForBD.Contracts.User;
using APIForBD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIForBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public praktikaContext Context { get; }

        public CategoryController(praktikaContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Вывод данных о всех категориях
        /// </summary>
        /// /// <param name="model">Категория</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() // Получение всех записей
        {
            List<Category> category = Context.Categories.ToList();
            return Ok(category);
        }
        /// <summary>
        /// Вывод данных об одной категории
        /// </summary>
        /// /// <param name="model">Категория</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Получение одной записи
        {
            Category? category = Context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(category);
        }
        /// <summary>
        /// Создание новой категории
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "categoryName" : "Фрукт",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Категория</param>
        /// <returns></returns>
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryRequest request) // Создание одной записи
        {
            var categoryDto = request.Adapt<Category>();

            Context.Categories.Add(categoryDto);
            Context.SaveChanges();
            return Ok(categoryDto);
        }
        /// <summary>
        /// Изменение текущих данных категории
        /// </summary>
        /// /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "categoryName" : "Фрукт",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// /// <param name="model">Категория</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateUserRequest request)// Изменение существующей записи
        {
            var categoryDto = request.Adapt<Category>();
            Context.Categories.Update(categoryDto);
            Context.SaveChanges();
            return Ok(categoryDto);
        }
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// /// <param name="model">Категория</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) // Удаление существующей записи
        {
            Category? category = Context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Not Found");
            }
            Context.Categories.Remove(category);
            Context.SaveChanges();
            return Ok(category);
        }
    }
}
