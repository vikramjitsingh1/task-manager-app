using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TmModule.Modules.Status.Models;

namespace TmModule.Infrastructure.DTOs
{
    public class AssignTaskToUserRequest
    {
        public required int[] TaskId { get; set; }
        public required int[] UserId { get; set; }
    }
}
