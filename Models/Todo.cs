
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class Todo
    {
        [Key]
        public int id { get; set; }

        [Required]

        public string NameToDo { get; set; }

        public bool isomplete { get; set; }

    }
}
