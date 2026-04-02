using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TmModule.Modules.Users.Models
{
    public class UsersTable
    {
        [Key]
        public int UserId { get; set; }
        public required string Username { get; set; }
    }
}
