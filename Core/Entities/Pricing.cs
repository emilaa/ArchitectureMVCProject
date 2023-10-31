using Core.Constants;
using DataAccess.Contexts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pricing : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Features { get; set; }
        public double Price { get; set; }
        public PricePer PricePer { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
    }
}
