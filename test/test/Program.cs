using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static test.testjson;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"C:\Users\C17 Developer\Documents\XavierProject\NgCooking\NgCooking\App_Data\Json\recettes.json");

            recettes[] o = JsonConvert.DeserializeObject<recettes[]>(text);
            int i = 0;
            foreach (var ligne  in o)
            {
                Console.WriteLine(ligne.name);
                Console.WriteLine(ligne.id);
                for (int j = 0; j< ligne.ingredients.Count(); j++)
                {
                    Console.WriteLine(ligne.ingredients[j]);
                }
                //Console.WriteLine(ligne.ingredients[0]);
                i++;
           
            }
            Console.WriteLine(i);  
            Console.ReadKey();
        }

    }
    class testjson
    {

        public class Rootobject
        {
            public recettes[] Property1 { get; set; }
        }

        public class recettes
        {
            public string id { get; set; }
            public string name { get; set; }
            public int creatorId { get; set; }
            public bool isAvailable { get; set; }
            public string picture { get; set; }
            public int calories { get; set; }
            public string[] ingredients { get; set; }
            public string preparation { get; set; }
            public Comment[] comments { get; set; }
        }

        public class Comment
        {
            public int userId { get; set; }
            public string title { get; set; }
            public string comment { get; set; }
            public int mark { get; set; }
        }

    }
}
