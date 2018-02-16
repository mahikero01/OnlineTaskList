using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Entities
{
    [Table("TaskLists")]
    public class TaskList
    {
        [Key]
        public Guid TaskListID { get; set; }

        public int UserID { get; set; }

        public String Title { get; set; }

        [MaxLength(50)]
        public String Description { get; set; }

        public DateTime DateCreated { get; set; }

        public Boolean IsDone { get; set; }
    }
}
