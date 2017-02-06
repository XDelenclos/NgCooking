using NgCooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NgCooking.Controllers
{
    public class CommunityController : Controller
    {
        NgCookingXEntities1 db = new NgCookingXEntities1();

        // GET: Community
        public ActionResult Communaute()
        {
            CommunityViewModels model = new CommunityViewModels();
            model.BestCooker = new List<BestCookers>();
            model.ListUser = new List<Communaute>();
            MeilleurCuistot(model.BestCooker);
            IndexCooker("a completer", model.ListUser);
            return View(model);
        }

        // GET: Community/id 
        public ActionResult Details(int id)
        {
            CommunityViewModels model = new CommunityViewModels();
            model.User = new Communaute();
            model.UserAge = new Users();
            model.RecetteUser = new List<Recettes>();
            RecetteUtilisateur(model.RecetteUser, id);
            DetailsUser(model.UserAge, id);
            model.User = db.Communaute.Find(id);            
            return View(model);
        }


        private void MeilleurCuistot(List<BestCookers> bestCooker, int limit = 8)
        {
            List<Communaute> cooker = db.Communaute.OrderByDescending(c => c.level).Take(limit).ToList();
            foreach (var item in cooker)
            {
                switch (item.level)
                {
                    case 1:
                        bestCooker.Add(new BestCookers
                        {
                            BestUser= item,
                            UserRate = "Novice"
                        });
                        break;
                    case 2:
                        bestCooker.Add(new BestCookers
                        {
                            BestUser = item,
                            UserRate = "Expert"
                        });
                        break;
                    case 3:
                        bestCooker.Add(new BestCookers
                        {
                            BestUser = item,
                            UserRate = "Confirmé"
                        });
                        break;
                }
            }
            





        }


        //private void MeilleurCuistot(List<BestCookers> bestCooker,int limit = 8)
        //{
        //    List<Communaute> cooker = db.Communaute.SqlQuery<string>("",)         
        //    foreach (var item in cooker)
        //    {
        //        if (item.Comment.Count > 0)
        //            bestCooker.Add(new BestCookers
        //            {
        //                BestUser = item,
        //                UserRate = (double)item.Comment.Average(r => r.mark)
        //            });
        //        else
        //        {
        //            bestCooker.Add(new BestCookers
        //            {
        //                BestUser = item,
        //                UserRate = 0
        //            });
        //        }
        //    }
        //}
        private void DetailsUser(Users user, int id)
        {
            string annee = db.Communaute.Find(id).birth;
            int age = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(annee));
            user.Age = age;
            user.Utilisateur = db.Communaute.Find(id) ;
        }

        private void RecetteUtilisateur(List<Recettes> Rec, int id, int limit = 4)
        {
            List<Recettes> recipe = db.Recettes.Where(c => c.creatorId == id).Take(limit).ToList();
            foreach (var item in recipe)
            {
                Rec.Add(item);
            }
        }

        private void IndexCooker(string choix, List<Communaute> listUser,int limit = 8)
        {
            switch (choix)
            {
                case "az":
                    listUser = db.Communaute.OrderBy(r => r.surname).Take(limit).ToList();
                    break;
                case "za":
                    listUser = db.Communaute.OrderByDescending(r => r.surname).Take(limit).ToList();
                    break;
                case "exp":
                    listUser = db.Communaute.OrderBy(r => r.Comment.Average(c => c.mark)).Take(limit).ToList();
                    break;
                case "prod":
                    listUser = db.Communaute.OrderByDescending(c => c.Recettes.Count()).Take(limit).ToList();
                    break;
                case "prod_desc":
                    listUser = db.Communaute.OrderBy(c => c.Recettes.Count()).Take(limit).ToList();
                    break;
            }

        }

    }
}