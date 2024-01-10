using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace count_the_variations
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Combination> data = new List<Combination>();        

            //Read from json and parse
            using(var reader = new StreamReader(@"flavors.json"))
            {
                var json = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<Combination>>(json);
            }

            var res = DetermineUniqueness(data);
            
            foreach(var key in res.Keys)
            {
                Console.WriteLine(String.Concat(key, " : ", res[key]));
            }
        }

             /// <summary>
        /// Takes a dataset of lists that contain combinations and determines how many
        /// occurances of each combination there is.
        /// <param> 
        /// data is a list of combinations that contains information about what flavors were consumed
        /// </param>
        /// </summary>
        public static Dictionary<string, int> DetermineUniqueness(List<Combination> data)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            foreach(var combination in data)
            {
                var set = new List<string>() {combination.FlavorOne, combination.FlavorTwo, combination.FlavorThree};
                set.Sort();
                var key = String.Join<string>(", ", set);
                if(res.ContainsKey(key)) //Dictonary already contains that combination update its value
                {
                    res[key] +=1;
                }
                else //Add new combination
                {
                    res.Add(key, 1);
                }
            }
            return res;
        }
 
        public struct Combination
        {
            public string FlavorOne {get;set;}
            public string FlavorTwo {get;set;}
            public string FlavorThree {get;set;}
        }
    }
}
