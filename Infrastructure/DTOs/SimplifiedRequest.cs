using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TmModule.Modules.Status.Models;

namespace TmModule.Infrastructure.DTOs
{
    public class SimplifiedRequest
    {
        public required string UserName { get; set; }
        public required string TaskDescription { get; set; }
        public TypeofStatus Status { get; set; } = TypeofStatus.Pending;
    }
}
