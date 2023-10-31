﻿using DataAccess.Contexts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AboutPhoto : BaseEntity
    {
        public string PhotoName { get; set; }
        public int Order { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
