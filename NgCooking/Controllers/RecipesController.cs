using Newtonsoft.Json;
using NgCooking.Models;
using NgCooking.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NgCooking.Controllers
{
    public class RecipesController : Controller
    {
        NgCookingXEntities1 db = new NgCookingXEntities1();
        RecipesViewModel model = new RecipesViewModel();

        public ActionResult Recettes()
        {

            EditionLastAndNewRecipes.MeilleuresRecettes(model.NewAndBest.lastRecipes);
            EditionLastAndNewRecipes.DernieresRecettes(model.NewAndBest.bestRecipes);
            var IngInList = db.Ingredient.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var Ing in IngInList)
            {
                var item = new SelectListItem
                {
                    Value = Ing.id.ToString(),
                    Text = Ing.name
                };
                items.Add(item);
            }
            MultiSelectList IngList = new MultiSelectList(items.OrderBy(i => i.Text), "Value", "Text");
            model.SelectIng = IngList;

            return View(model);
        }

        // POST: 
        [HttpPost]
        public ActionResult Recettes(RecipesViewModel submitted)
        {
            model = DisplaySearchResult(submitted);
            var IngInList = db.Ingredient.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var Ing in IngInList)
            {
                var item = new SelectListItem
                {
                    Value = Ing.id.ToString(),
                    Text = Ing.name
                };
                items.Add(item);
            }
            MultiSelectList IngList = new MultiSelectList(items.OrderBy(i => i.Text), "Value", "Text");
            model.SelectIng = IngList;
            EditionLastAndNewRecipes.MeilleuresRecettes(model.NewAndBest.lastRecipes);
            EditionLastAndNewRecipes.DernieresRecettes(model.NewAndBest.bestRecipes);

            return View(model);
        }


        public ActionResult NewRecipes()
        {
            return View();
        }

        public ActionResult RecipesDetails(string nom)
        {
            model.recette = new Recettes();
            model.UsersComment = new List<CommentsByUser>();
            model.recette = db.Recettes.Find(nom);

            if (model != null)
                return View(model);
            else
                return View("page not found");
        }

        public void IndexRecipes(string choix, List<Recettes> ListRecipes, int limit = 8)
        {

            switch (choix)
            {
                case "az":
                    ListRecipes = db.Recettes.OrderBy(r => r.name).Take(limit).ToList();
                    break;
                case "za":
                    ListRecipes = db.Recettes.OrderByDescending(r => r.name).Take(limit).ToList();
                    break;
                case "new":
                    ListRecipes = db.Recettes.OrderBy(r => r.id).Take(limit).ToList();
                    break;
                case "old":
                    ListRecipes = db.Recettes.OrderByDescending(r => r.id).Take(limit).ToList();
                    break;
                case "best":
                    ListRecipes = db.Recettes.OrderBy(c => c.Comment.Average(m => m.mark)).Take(limit).ToList();
                    break;
                case "worst":
                    ListRecipes = db.Recettes.OrderBy(c => c.Comment.Average(m => m.mark)).Take(limit).ToList();
                    break;
                case "cal":
                    ListRecipes = db.Recettes.OrderBy(c => c.calories).Take(limit).ToList();
                    break;
                case "cal_desc":
                    ListRecipes = db.Recettes.OrderBy(c => c.calories).Take(limit).ToList();
                    break;
            }

        }
        
        //Get
        [HttpGet]
        public ActionResult RecipesIndexer(string id, int limit = 8)
        {
            var test = Request.QueryString["id"];
            string[] donnée = test.Split('/');             
            model.Result = new List<Recettes>();
            model.IngIDs = new List<string>();
            for (int i = 1; i < donnée.Length-1; i++)
            {
               model.IngIDs.Add(donnée[i]);
                
            }
            foreach (var item in model.IngIDs)
            {
                model.Result.AddRange(db.Recettes.Where(r => r.id == item));
            }
            
            Dictionary<string, List<Recettes>> index = new Dictionary<string, List<Recettes>>();
            index.Add("az", model.Result.OrderBy(r => r.name).Take(limit).ToList());
            index.Add("za", model.Result.OrderByDescending(r => r.name).Take(limit).ToList());
            index.Add("new", model.Result.OrderBy(r => r.id).Take(limit).ToList());
            index.Add("old", model.Result.OrderByDescending(r => r.id).Take(limit).ToList());
            index.Add("best", model.Result.OrderBy(c => c.Comment.Average(m => m.mark)).Take(limit).ToList());
            index.Add("worst", model.Result.OrderByDescending(c => c.Comment.Average(m => m.mark)).Take(limit).ToList());
            index.Add("cal", model.Result.OrderBy(c => c.calories).Take(limit).ToList());
            index.Add("cal_desc", model.Result.OrderByDescending(c => c.calories).Take(limit).ToList());
            model.Result = new List<Recettes>(index[donnée[0]]);
            return PartialView("_AffichageListe",model);
        }

        #region methode

        public CommentsByUser CommentUsers(Recettes rec, List<CommentsByUser> Blogger)
        {
            CommentsByUser item = new CommentsByUser();
            return item;
        }

        public RecipesViewModel DisplaySearchResult(RecipesViewModel submitted, int limit = 4)
        {
            List<Recettes> RecipesByIng = new List<Recettes>();
            submitted.Result = new List<Recettes>();
            if (submitted.IngIDs != null || submitted.Name != null || (submitted.CalorieMin >= 0 && submitted.CalorieMax != 0))
            {

                if (submitted.IngIDs != null)
                {

                    //ajoute a la liste les recettes contenants les ingrédients sélectionnés
                    foreach (var item in submitted.IngIDs)
                    {
                        RecipesByIng.AddRange(db.Recettes.Where(n => n.Ingredient.Any(m => m.id.Contains(item))));
                    }
                    if (submitted.IngIDs.Count() > 1)
                    {
                        // cette requête permet de repérer les Id de recettes dupliqués ( donc l'id des recettes correspondant à notre recherche)
                        var duplicatedID = from r in RecipesByIng
                                           group r by r.id into g
                                           where g.Count() > 1
                                           select g.Key;
                        //requête nous renvoyant la liste des recettes en doublons
                        var duplicated = RecipesByIng.FindAll(p => duplicatedID.Contains(p.id));
                        //supression des doublons dans la listes 
                        submitted.Result = duplicated.Distinct(new DistinctItemComparer()).ToList();
                    }
                    else
                        submitted.Result = RecipesByIng;
                }
                //Recherche par nom de recette 
                if (submitted.Name != String.Empty)

                    if (submitted.Result == null)
                        submitted.Result.RemoveAll(m => !m.name.ToLower().Contains(submitted.Name.ToLower()));

                    else
                        submitted.Result.AddRange(db.Recettes.Where(m => m.name.ToLower().Contains(submitted.Name.ToLower())).ToList());

                //Recherche par Calorie
                if (submitted.CalorieMin >= 0 && submitted.CalorieMax != 0)

                    if (submitted.CalorieMin < submitted.CalorieMax)

                        if (submitted.Result == null)
                        {
                            submitted.Result.RemoveAll(m => m.calories >= submitted.CalorieMax);
                            submitted.Result.RemoveAll(m => m.calories <= submitted.CalorieMin);
                        }
                        else
                        {
                            submitted.Result.AddRange(db.Recettes.Where(m => m.calories <= submitted.CalorieMax).ToList());
                            submitted.Result.RemoveAll(m => m.calories <= submitted.CalorieMin);
                        }

                    else
                        submitted.Result = null;
            }
            else
                submitted.Result = db.Recettes.OrderBy(n => n.name).ToList();

            return submitted;

        }
    }


    class DistinctItemComparer : IEqualityComparer<Recettes>
    {
        public bool Equals(Recettes x, Recettes y)
        {
            return x.id == y.id &&
                x.calories == y.calories &&
                x.Comment == y.Comment &&
                x.Communaute == y.Communaute &&
                x.creatorId == y.creatorId &&
                x.Ingredient == y.Ingredient &&
                x.isAvailable == y.isAvailable &&
                x.name == y.name &&
                x.picture == y.picture &&
                x.preparation == y.preparation;
        }

        public int GetHashCode(Recettes obj)
        {
            return obj.id.GetHashCode() ^
                obj.calories.GetHashCode() ^
                obj.Comment.GetHashCode() ^
                obj.Communaute.GetHashCode() ^
                obj.creatorId.GetHashCode() ^
                obj.Ingredient.GetHashCode() ^
                obj.isAvailable.GetHashCode() ^
                obj.name.GetHashCode() ^
                obj.picture.GetHashCode() ^
                obj.preparation.GetHashCode();
        }
    }
    #endregion
}