using NPOI.SS.Formula.Functions;
using TodoApp.Hepper;
using TodoApp.Interface;
using TodoApp.Models;
namespace TodoApp.actions
{
    public class TodoAction : ITodo
    {
        private readonly TodoAppConText ContextDb = new TodoAppConText();

        public IQueryable<Todo> GetDoDo(OwnerParameters ownerParameters)
        {
            var listTodo = ContextDb.Todos.Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize).Take(ownerParameters.PageSize).AsQueryable();

            return listTodo;
        }
        public PagedList<Todo> GetTodo(AccountParameters ownerParameters)
        {
            if(ContextDb.Todos.Count() == 0)
            {
                return null;
            }
            if(ownerParameters.searchParam == null)
            {
                ownerParameters.searchParam = "";
            }
            return PagedList<Todo>.ToPagedList(ContextDb.Todos.Where(x=>x.NameToDo.ToLower().Contains(ownerParameters.searchParam.ToLower())).OrderByDescending(x=>x.id).AsQueryable(),
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
        }
        public Todo GetOneTodo(int id)
        {
            if (ContextDb.Todos.Count() == 0)
            {
                return null;
            }
            var TodoOne = ContextDb.Todos.SingleOrDefault(x=>x.id == id);
            if(TodoOne != null)
            {
                return TodoOne;
            }
            return null;
        }
        public Todo setTodo(Todo todoCreate)
        {
            if(ContextDb.Todos.Any(x=>x.NameToDo.ToLower() == todoCreate.NameToDo.ToLower()))
            {
                return null;
            }
           using(var trans = ContextDb.Database.BeginTransaction())
            {
                try
                {
                    ContextDb.Todos.Add(todoCreate);
                    ContextDb.SaveChanges();
                    trans.Commit();
                    return todoCreate;
                }catch(Exception error)
                {
                    trans.Rollback();
                    return null;
                }
            }
            
        }
        public string DleToDoId(int Id)
        {
            var TodoDle = ContextDb.Todos.SingleOrDefault(x => x.id == Id);
            if(TodoDle != null)
            {
                ContextDb.Todos.Remove(TodoDle);
                ContextDb.SaveChanges();
                return "bạn đã xóa thành công";
            }
            return null;
        }

        public Todo UpdateStatus(Todo todoUpdate)
        {
            todoUpdate = ContextDb.Todos.SingleOrDefault(x => x.id == todoUpdate.id);
            if(todoUpdate != null)
            {
                todoUpdate.isomplete = !todoUpdate.isomplete;
                ContextDb.Todos.Update(todoUpdate);
                ContextDb.SaveChanges();
                return todoUpdate;

            }
            return null;
        }
    }
}
