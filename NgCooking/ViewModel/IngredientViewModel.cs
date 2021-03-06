﻿using NgCooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NgCooking.Models
{
    public class IngredientViewModel
    {
        public Ingredient Ingredient { get; set; }
        public IngredientModel SearchIngredient { get; set; }
        public List<Categories> ListCat { get; set; }
        public List<Ingredient> ListIng { get; set; }
        public List<DisplayedIngredients> Result { get; set; }

    }
    public class DisplayedIngredients
    {
        public int level { get; set; }
        public Ingredient ingredient { get; set; }
        public List<Ingredient> SimIngredient { get; set; }

    }

    public class IngredientModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Min { get; set; } = 0;
        public int Max { get; set; }
    }
}