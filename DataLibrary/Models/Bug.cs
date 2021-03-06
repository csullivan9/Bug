﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Bug
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string BugSeverity { get; set; }

        public string Status { get; set; }
    }
}
