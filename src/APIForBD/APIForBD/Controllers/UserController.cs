using APIForBD.Contracts.User;
using APIForBD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIForBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public praktikaContext Context { get; }

        public UserController(praktikaContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Вывод данных о всех пользователях
        /// </summary>
        /// /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() // Получение всех записей
        {
            List<User> user = Context.Users.ToList();
            return Ok(user);
        }
        /// <summary>
        /// Вывод данных об одном конкретном пользователе
        /// </summary>
        /// /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Получение одной записи
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(user);
        }
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "surname" : "Иванов",
        ///        "name" : "Иван",
        ///        "password" : "!Pa$$word123@",
        ///        "email" : "21314@gmail.com",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request) // Создание одной записи
        {
            var userDto = request.Adapt<User>();
            Context.Users.Add(userDto);
            Context.SaveChanges();
            return Ok(userDto);
        }
        /// <summary>
        /// Изменение текущих данных пользователя
        /// </summary>
        /// /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "surname" : "Иванов",
        ///        "name" : "Иван",
        ///        "password" : "!Pa$$word123@",
        ///        "email" : "21314@gmail.com",
        ///        "createdBy": 1,
        ///        "createdAt": "2024-01-23T06:57:38.334Z"
        ///     }
        ///
        /// </remarks>
        /// /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateUserRequest request)// Изменение существующей записи
        {
            var userDto = request.Adapt<User>();
            Context.Users.Update(userDto);
            Context.SaveChanges();
            return Ok(userDto);
        }
        /// <summary>
        /// Удаление данных о выбранном пользователе
        /// </summary>
        /// /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) // Удаление существующей записи
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok(user);
        }
    }
}
