using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NgCooking
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // /community/
            routes.MapRoute(
                name: "Communaute",
                url: "Community",
                defaults: new { controller = "Community", action = "Communaute", id = UrlParameter.Optional }
            );

            // /Community/nom-utilisateur
            routes.MapRoute(
                 name: "Details",
                 url: "Community/{id}",
                 defaults: new { controller = "Community", action = "Details", id = UrlParameter.Optional }
             );

            // /Ingredient/
            routes.MapRoute(
                 name: "Ingredient",
                 url: "Ingredient/",
                 defaults: new { controller = "Ingredient", action = "Ingredients", id = UrlParameter.Optional }
             );            
                                    
            // /Recipes/
            routes.MapRoute(
                name: "Recettes",
                url: "Recipes",
                defaults: new { controller = "Recipes", action = "recettes", id = UrlParameter.Optional }
            );

            // /Recipes/tri
            routes.MapRoute(
                name: "TriRecettes",
                url: "Recipes/RecipesIndexer/{id}",
                defaults: new { controller = "Recipes", action = "RecipesIndexer", id = UrlParameter.Optional }
            );
            //  /Recipes/nom-de-la-recette
            routes.MapRoute(
                name: "Détails",
                url: "Recipes/{nom}",
                defaults: new { controller = "Recipes", action = "recipesDetails", nom = "" }
            );

            // /Recipes/new
            routes.MapRoute(
                name: "Nouvelle recette",
                url: "Recipes/new",
                defaults: new { controller = "Recipes", action = "newRecipes", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
