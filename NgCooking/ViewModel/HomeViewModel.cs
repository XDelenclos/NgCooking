using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NgCooking.Models
{
    public class HomeViewModel
    {
        public BestRecipeModel LastAndBest { get; set; } =  new BestRecipeModel();
    }
}