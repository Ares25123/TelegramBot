using APIForBD.Contracts.Meal;
using APIForBD.Contracts.User;
using APIForBD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIForBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        public praktikaContext Context { get; }

        public MealController(praktikaContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Вывод данных о всех блюдах
        /// </summary>
        /// /// <param name="model">Блюдо</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() // Получение всех записей
        {
            List<Meal> meal = Context.Meals.ToList();
            return Ok(meal);
        }
        /// <summary>
        /// Вывод данных об одном блюде
        /// </summary>
        /// /// <param name="model">Блюдо</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Получение одной записи
        {
            Meal? meal = Context.Meals.Where(x => x.MealId == id).FirstOrDefault();
            if (meal == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(meal);
        }
        /// <summary>
        /// Создание нового блюда
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "MealName" : "Яблочный пирог",
        ///        "MealDescription" : "Сделан из яблок и теста",
        ///        "PreparationTime" : "01:15:00",
        ///        "KKAL" : "265.5",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Блюдо</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateMealRequest request) // Создание одной записи
        {
            var mealDto = request.Adapt<Meal>();
            Context.Meals.Add(mealDto);
            Context.SaveChanges();
            return Ok(mealDto);
        }
        /// <summary>
        /// Изменение текущих данных блюда
        /// </summary>
        /// /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "MealName" : "Яблочный пирог",
        ///        "MealDescription" : "Сделан из яблок и теста",
        ///        "PreparationTime" : "01:15:00",
        ///        "KKAL" : "265.5",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// /// <param name="model">Блюдо</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateMealRequest request)// Изменение существующей записи
        {
            var mealDto = request.Adapt<Meal>();
            Context.Meals.Update(mealDto);
            Context.SaveChanges();
            return Ok(mealDto);
        }
        /// <summary>
        /// Удаление данных о выбранном блюде
        /// </summary>
        /// /// <param name="model">Блюдо</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) // Удаление существующей записи
        {
            Meal? meal = Context.Meals.Where(x => x.MealId == id).FirstOrDefault();
            if (meal == null)
            {
                return BadRequest("Not Found");
            }
            Context.Meals.Remove(meal);
            Context.SaveChanges();
            return Ok(meal);
        }
    }
}
