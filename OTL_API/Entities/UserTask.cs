using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Entities
{
    [Table("UserTasks")]
    public class UserTask
    {
        [Key]
        public Guid UserTaskID { get; set; }

        [MaxLength(450)]
        public String UserID { get; set; }

        public String Title { get; set; }

        [MaxLength(50)]
        public String Description { get; set; }

        public DateTime DateCreated { get; set; }

        public Boolean IsDone { get; set; }
    }
}
