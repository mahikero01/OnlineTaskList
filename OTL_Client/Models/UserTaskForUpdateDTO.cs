﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTL_Client.Models
{
    public class UserTaskForUpdateDTO
    {
        public String UserID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Boolean IsDone { get; set; }
    }
}
