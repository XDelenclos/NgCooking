using Newtonsoft.Json;
using NgCooking.Models;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using static NgCooking.Models.BestRecipeModel;
using NgCooking.Tools;

namespace NgCooking.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();     
            EditionLastAndNewRecipes.MeilleuresRecettes(model.LastAndBest.lastRecipes);
            EditionLastAndNewRecipes.DernieresRecettes(model.LastAndBest.bestRecipes);
            return View(model);
        }

        public static void seeding()
        {
            var db = new NgCookingXEntities1();
            string json = "";
            using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\categories.json"))
            {
                json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (dynamic item in array)
                {
                    var Cat = new Categories
                    {
                        id = item.id,
                        name = item.nameToDisplay,
                    };
                    db.Categories.Add(Cat);
                }

            }

            using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\communaute.json"))
            {
                json = r.ReadToEnd();
                List<Communaute> ListUser = JsonConvert.DeserializeObject<List<Communaute>>(json);

                foreach (var Commu in ListUser)
                {
                    db.Communaute.Add(Commu);
                }
            }

            using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\ingredients.json"))
            {
                json = r.ReadToEnd();
                List<Ingredient> ListIng = JsonConvert.DeserializeObject<List<Ingredient>>(json);
                foreach (var Ing in ListIng)
                {
                    db.Ingredient.Add(Ing);
                }
                //dynamic array = JsonConvert.DeserializeObject(json);
                //foreach (dynamic item in array)
                //{
                //    var Ing = new Ingredient();

                //    Ing.id = item.id;
                //    Ing.name = item.name;
                //    Ing.isAvailable = item.isAvailable;
                //    Ing.picture = item.picture;
                //    Ing.category = item.category.Value;
                //    Ing.calories = item.calories;

                //    db.Ingredient.Add(Ing);
                //}

            }

            using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\recettes.json"))
            {
                json = r.ReadToEnd();
                List<Recettes> ListRecipes = JsonConvert.DeserializeObject<List<Recettes>>(json);

                foreach (var Recip in ListRecipes)
                {
                    db.Recettes.Add(Recip);
                }
            }
            db.SaveChanges();



            //}            
            //using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\recettes.json"))
            //{
            //    json = r.ReadToEnd();
            //    dynamic array = JsonConvert.DeserializeObject(json);
            //    foreach (dynamic item in array)
            //    {
            //        var com = new Comment
            //        {
            //            comment1 = item.comment,
            //            userID = item.userId,
            //            recipeId = item.id,
            //            title = item.title,
            //            mark = (int)item.mark
            //        };
            //        db.Comment.Add(com);
            //    }
            //db.SaveChanges();
        }
    }

}

