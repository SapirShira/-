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
        public HashSet<String> lst_dupNames { get; set; }

        public void print_lst(List<string> lst_toPrint)
         {
             
            foreach(string n in lst_toPrint)
            {
                Console.Write(n + ' ');
            }
             
         }

        public Dictionary<String, List<string>> fixListName()
        {
            Dictionary<String, List<string>> map_fix = new Dictionary<string, List<string>>();
            int flag = 0;
            this.lst_dupNames = new HashSet<string>();
            foreach (List<string> l_n in this.lst_sn)
            {
                this.lst_dupNames.Add(l_n[0]);
                this.lst_dupNames.Add(l_n[1]);
                flag = 0;
                if (map_fix.ContainsKey(l_n[0]))
                    map_fix.GetValueOrDefault(l_n[0]).Add(l_n[1]);
                else if (map_fix.ContainsKey(l_n[1]))
                    map_fix.GetValueOrDefault(l_n[1]).Add(l_n[0]);
                else
                {
                    foreach(string k in map_fix.Keys)
                    {
                        if (map_fix.GetValueOrDefault(k).Contains(l_n[0]))
                        {
                            map_fix.GetValueOrDefault(k).Add(l_n[1]);
                            flag = 1;
                            break;
                        }
                        else if (map_fix.GetValueOrDefault(k).Contains(l_n[1]))
                        {
                            map_fix.GetValueOrDefault(k).Add(l_n[0]);
                            flag = 1;
                            break;
                        }
                    }
                    if(flag == 0 )
                    {
                        map_fix.Add(l_n[0], new List<string>() { l_n[1] });
                    }
                }
            }
            return map_fix;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int flag = 0;
            string enterdValue;
            Dictionary<string, int> new_map = new Dictionary<string, int>();
            fix_list f = new fix_list();
            f.map_name = new Dictionary<string, int>();
            //{ { "Jacob", 15 }, { "Yaakov", 12 }, { "Tomer", 13 }, { "Tommer", 4 }, { "Sara", 19 } };
            f.lst_sn = new List<List<string>>();
            //f.lst_sn.Add(new List<string> { "Jacob", "Yaakov" });
            //f.lst_sn.Add(new List<string> { "Yaakov", "Yaacov" });
            //f.lst_sn.Add(new List<string> { "Tomer", "Tommer" });
            Console.WriteLine("Enter a name and number with a space between them - each pair in a separate row or -1 to the end of the list");
            
            while (flag != -1)
            {
                enterdValue = Console.ReadLine();
                if (enterdValue[0] == ' ')
                {
                    Console.WriteLine("please Enter a valid value with now space in the begining");
                }
                
                if (enterdValue.Contains("-1"))
                {
                    flag = -1;
                    continue;
                }
                string[] enterdVal = enterdValue.Split(' ');
                f.map_name.Add(enterdVal[0], int.Parse(enterdVal[1]));
            }

            Console.WriteLine("Enter pairs of similar names with a space between them - each pair in a separate row or -1 to the end of the list");
            flag = 0;
            while (flag != -1)
            {
                enterdValue = Console.ReadLine();
                if (enterdValue[0] == ' ')
                {
                    Console.WriteLine("please Enter a valid value with now space in the begining");
                }

                if (enterdValue.Contains("-1"))
                {
                    flag = -1;
                    continue;
                }
                string[] enterdVal = enterdValue.Split(' ');
                f.lst_sn.Add(new List<string>() { enterdVal[0], enterdVal[1]});
            }

            Dictionary<string, List<string>> map_fix = f.fixListName();
           
            foreach (string k in map_fix.Keys)
            {
                new_map.Add(k, 0);
                
            }
            foreach(string n in f.map_name.Keys)
            {
                if(new_map.ContainsKey(n))
                {
                    new_map[n] += f.map_name.GetValueOrDefault(n);
                }
                else if(f.lst_dupNames.Contains(n))
                {
                    foreach(string k in map_fix.Keys)
                    {
                        if(map_fix.GetValueOrDefault(k).Contains(n))
                            new_map[k] += f.map_name.GetValueOrDefault(n);
                    }
                }
                else
                {
                    new_map.Add(n, f.map_name.GetValueOrDefault(n));
                }
            }
            foreach (string k in new_map.Keys)
            {
                Console.WriteLine(k + ' ' + new_map.GetValueOrDefault(k));
            }

        }
    }
}
