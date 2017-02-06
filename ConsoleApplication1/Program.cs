using NgCooking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string  json = "";
            using (StreamReader r = new StreamReader(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\categories.json"))
            {
                json = r.ReadToEnd();
                List<Categories> items = JsonConvert.DeserializeObject<List<Categories>>(json);
                
            }
            using (var db = new NgCookingXEntities())
            {
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (dynamic item in array)
                {
                    var cat = new Categories
                    {
                        id = item.id,
                        name = item.nameToDisplay
                    };
                    
                    db.Categories.Add(cat);                   
                    
                }
                db.Database.Log = Console.WriteLine;
                db.SaveChanges();
                Console.ReadKey();
            }
        }
    }
}
