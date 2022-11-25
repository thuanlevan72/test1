using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApp.actions;
using TodoApp.Hepper;
using TodoApp.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoAction ControllerAction = new TodoAction();
        // GET: api/<TodoController>
        [HttpGet]
        public IActionResult Get([FromQuery] AccountParameters ownerParameters)
        {
            var Todos = ControllerAction.GetTodo(ownerParameters);
            if(Todos == null)
            {
                return Ok(Todos);
            }

            var metadata = new
            {
                 pageRes = new
                {
                     Todos.TotalCount,
                     Todos.PageSize,
                     Todos.CurrentPage,
                     Todos.TotalPages,
                     Todos.HasNext,
                     Todos.HasPrevious,
                 },
                Todos
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata.pageRes));
          
            return Ok(metadata);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var GetOneTodo = ControllerAction.GetOneTodo(id);
            if (GetOneTodo == null)
            {
                return NotFound("không có dữ liệu nào cả");
            }
            return Ok(GetOneTodo);
        }

        // POST api/<TodoController>
        [HttpPost]
        public IActionResult Post([FromBody]Todo TodoCreate)
        {
            var resTodo = ControllerAction.setTodo(TodoCreate);
            if(resTodo == null)
            {
                return BadRequest("thêm thất bại");
            }
            else
            {
                return Ok(resTodo);
            }
        }

        // PUT api/<TodoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Todo ToDoUpdate)
        {
            var Todos = ControllerAction.UpdateStatus(ToDoUpdate);
            if(Todos == null)
            {
                return Ok(ToDoUpdate);
            }
            return Ok(Todos);

        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string DleToDoId = ControllerAction.DleToDoId(id);
            if(DleToDoId == null)
            {
                return NotFound("bạn đã xóa thất bại");
            }
            else
            {
                return Ok(DleToDoId);
            }
        }
    }
}
