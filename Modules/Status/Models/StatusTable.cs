using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TmModule.Application.CoreController;
using TmModule.Modules.Tasks.Models;
using TmModule.Modules.Users.Models;

namespace TmModule.Modules.Status.Models
{
    
    public class StatusTable
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public TypeofStatus CurrentStatus { get; set; }

        public TaskTable? Task { get; set; }
        public UsersTable? Users { get; set; }
    }
}
