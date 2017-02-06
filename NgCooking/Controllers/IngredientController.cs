using Newtonsoft.Json;
using NgCooking.Models;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using static NgCooking.Models.IngredientModel;

namespace NgCooking.Controllers
{
    public class IngredientController : Controller
    {
        NgCookingXEntities1 db = new NgCookingXEntities1();
        IngredientViewModel model = new IngredientViewModel();

        public ActionResult Ingredients()
        {
            model.ListCat = db.Categories.ToList();
            return View(model);
        }

        // POST: 
        [HttpPost]
        public ActionResult Ingredients(string NameFilter = "", string CategoryFilter = "", int CalorieMin=0 , int CalorieMax = 10000)
        {
            IngredientModel IngMod = new IngredientModel();
            IngMod.Category = CategoryFilter;
            IngMod.Name = NameFilter;
            IngMod.Max = CalorieMax;
            IngMod.Min = CalorieMin;

            model = DisplayResult(IngMod);
            model.ListCat = db.Categories.ToList();
            return View(model);
        }

        public IngredientViewModel DisplayResult(IngredientModel IngMod, int limit = 4)
        {
            IngredientViewModel model = new IngredientViewModel()
            {
                ListIng = db.Ingredient.OrderBy(c => c.calories).ToList()
            };


            if (IngMod.Category != String.Empty)
                model.ListIng.RemoveAll(m => !m.category.ToLower().Contains(IngMod.Category.ToLower()));

            if (IngMod.Name != String.Empty)
                model.ListIng.RemoveAll(m => !m.name.ToLower().Contains(IngMod.Name.ToLower()));

            if (IngMod.Min >= 0 && IngMod.Max != 0)
            {
                if (IngMod.Min < IngMod.Max)
                {
                    model.ListIng.RemoveAll(m => m.calories >= IngMod.Max);
                    model.ListIng.RemoveAll(m => m.calories <= IngMod.Min);
                }
                else
                    model = new IngredientViewModel();
                
            }


            if (model.ListIng != null)
            {
                model.Result = new List<DisplayedIngredients>();
                foreach (var item in model.ListIng)
                {
                    int lvlCal=0;
                    if (model.ListIng.Count > 1)
                        lvlCal = ((item.calories.Value) * 100) /(db.Ingredient.Where(c => c.category == item.category).Max(c => c.calories).Value) ;
                    
                    else
                        lvlCal = 100;
                   
                    model.Result.Add(new DisplayedIngredients
                    {
                        level = lvlCal,
                        ingredient = item,
                        SimIngredient = db.Ingredient.Where(c => c.category == item.category && c.id != item.id).OrderBy(c => c.calories).Take(limit - 1).ToList()
                    });
                }
            }

            return model;
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}