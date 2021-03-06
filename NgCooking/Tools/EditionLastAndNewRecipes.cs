﻿using NgCooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static NgCooking.Models.BestRecipeModel;

namespace NgCooking.Tools
{
    public static class EditionLastAndNewRecipes
    {        
        public static void MeilleuresRecettes(List<BestRecipes> thelast, int limit = 4)
        {
            NgCookingXEntities1 db = new NgCookingXEntities1();
            /*List<Recettes> derniere = db.Recettes.OrderByDescending(c => c.id).Take(limit).ToList();
             
            2eme possibilité pour vérifier le bon fonctionnement ( classement par ordre alphabétique)*/
            List<Recettes> BestRate = db.Recettes.OrderBy(c => c.id).Take(limit).ToList();

            foreach (var rec in BestRate)
            {
                if (rec.Comment.Count > 0)
                    thelast.Add(new BestRecipes
                    {
                        bestRecette = rec,
                        rate = (decimal)rec.Comment.Average(r => r.mark)
                    });
                else
                {
                    thelast.Add(new BestRecipes
                    {
                        bestRecette = rec,
                        rate = 0
                    });
                }
            }
        }
        public static void DernieresRecettes(List<BestRecipes> recettes, int limit = 4)
        {
            NgCookingXEntities1 db = new NgCookingXEntities1();
            List<Recettes> BestRate = db.Recettes.OrderByDescending(c => c.Comment.Average(r => r.mark)).Take(limit).ToList();

            foreach (var rec in BestRate)
            {
                if (rec.Comment.Count > 0)
                    recettes.Add(new BestRecipes
                    {
                        bestRecette = rec,
                        rate = (decimal)rec.Comment.Average(r => r.mark)
                    });
                else
                {
                    recettes.Add(new BestRecipes
                    {
                        bestRecette = rec,
                        rate = 0
                    });
                }

            }

        }
    }
}