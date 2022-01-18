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
        public List<Similar_name> lst_sn { get; set; }
        public Dictionary<string, int> map_name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Similar_name s = new Similar_name();
            s.name_1 = "try 1";
            s.name_2 = "try 2";
            Console.WriteLine(s.name_1 + ' ' + s.name_2);
        }
    }
}
