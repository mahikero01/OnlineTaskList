using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_API.Models
{
    public class TaskListDTO
    {
        public Guid TaskListID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public DateTime DateCreated { get; set; }

        public Boolean IsDone { get; set; }
    }
}
