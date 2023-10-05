using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesTodo.Web.Models
{
    public class Todo
    {
        public Todo()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
