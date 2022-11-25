using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using NPOI.SS.Formula.Functions;
using TodoApp.Hepper;
using TodoApp.Models;

namespace TodoApp.Interface
{
    public interface ITodo
    {
        public IQueryable<Todo> GetDoDo(OwnerParameters ownerParameters);
        public Todo GetOneTodo(int id);
        public Todo setTodo (Todo todoCreate);
        public string DleToDoId(int Id);
        public Todo UpdateStatus(Todo todoUpdate);
    }
}
