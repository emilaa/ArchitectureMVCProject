using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
        public Basket Basket { get; set; }

    }
}
