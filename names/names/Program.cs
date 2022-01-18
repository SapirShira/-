using System;
using System.Collections.Generic;

namespace names
{
    public class Similar_name
    {
        public string name_1 { get; set; }
        public string name_2 { get; set; }
    }

    public class fix_list
    {
        public List<List<string>> lst_sn { get; set; }
        public Dictionary<string, int> map_name { get; set; }
         public void print_lst()
         {
             foreach (List<string> d in lst_sn)
             {
                 foreach(string n in d)
                 {
                     Console.Write(n + ' ');
                 }
                 Console.WriteLine();
             }
         }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            fix_list f = new fix_list();
            f.map_name = new Dictionary<string, int>() { { "Jacob", 15 }, { "Yaakov", 12 }, { "Tomer", 13 }, { "Tommer", 4 }, { "Sara", 19 } };
            f.lst_sn = new List<List<string>>();
            f.lst_sn.Add(new List<string> { "Jacob", "Yaakov" });
            f.lst_sn.Add(new List<string> { "Yaakov", "Yaacov" });
            f.lst_sn.Add(new List<string> { "Tomer", "Tommer" });
            foreach (KeyValuePair<string, int> d in f.map_name)
            {
                Console.WriteLine(d.Key + ' ' + d.Value);

            }
            f.print_lst();

        }
    }
}
