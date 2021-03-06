﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_Client.Models
{
    public class UserTask
    {
        public Guid UserTaskID { get; set; }

        public String UserID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        [Display(Name ="Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Done ?")]
        public Boolean IsDone { get; set; }
    }
}
