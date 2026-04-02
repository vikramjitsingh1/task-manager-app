using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TmModule.Modules.Tasks.Models
{
    public class TaskTable
    {
        [Key]
        public int TaskId { get; set; }
        public string? Description { get; set; }
    }
}
