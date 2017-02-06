﻿using System.Web;
using System.Web.Optimization;

namespace NgCooking
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-select.css", 
                      "~/Content/site.css",
                      "~/Content/style.css",
                      "~/Content/reset.css"));
            bundles.Add(new ScriptBundle("~/bundles/CustomScript").Include(
                        "~/Scripts/ScriptsJs/IngredientsScript.js",
                        "~/Scripts/ScriptsJs/RecipeScript.js",
                        "~/Scripts/ScriptsJs/_LayoutScripts.js",
                        "~/Scripts/ScriptsJs/CommunityScript.js"));
        }
    }
}