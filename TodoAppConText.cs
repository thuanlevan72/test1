using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp
{
    public class TodoAppConText: DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-31L1DE9\\SQLEXPRESS; Database=todos; integrated security=sspi;");
        }
    }
}
