using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


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
            int work = 0;
            string enterdValue;
            Dictionary<string, int> new_map = new Dictionary<string, int>();
            fix_list f = new fix_list();
            f.map_name = new Dictionary<string, int>();
            f.lst_sn = new List<List<string>>();
            
            while (work == 0)
            {
                string text;
                try
                {
                    Console.WriteLine("Enter the path of the file");
                    enterdValue = Console.ReadLine();

                    text = System.IO.File.ReadAllText(enterdValue);
                }
                catch (Exception e)
                {
                    Console.WriteLine("path is not valid please try agine");
                    continue;
                }
                try
                {
                    string[] splitedFil = text.Split("Synonyms:");
                    string[] splitFirstPart = splitedFil[0].Split("Names:");
                    string[] splitToMap;
                    splitFirstPart = splitFirstPart[1].Split(',');
                    string name;
                    int num;
                    for (int i = 0; i < splitFirstPart.Length; i++)
                    {
                        name = "";
                        num = 0;
                        splitToMap = splitFirstPart[i].Split('(');
                        for (int j = 0; j < splitToMap.Length; j++)
                        {
                            splitToMap[j] = Regex.Replace(splitToMap[j], @"[^0-9a-zA-Z]+", "").ToString();
                            if (splitToMap[j] != "")
                            {
                                if (name == "")
                                    name = splitToMap[j];
                                else
                                {
                                    num = Convert.ToInt32(splitToMap[j]);
                                    break;
                                }
                            }
                        }
                        if (name != "")
                            f.map_name.Add(name, num);
                    }

                    string[] splitScondPart = splitedFil[1].Split(',');
                    for (int i = 0; i < splitScondPart.Length -1; i+=2)
                    {
                        splitScondPart[i] = Regex.Replace(splitScondPart[i], @"[^0-9a-zA-Z]+", "").ToString();
                        splitScondPart[i+1] = Regex.Replace(splitScondPart[i+1], @"[^0-9a-zA-Z]+", "").ToString();

                        f.lst_sn.Add(new List<string>() { splitScondPart[i], splitScondPart[i + 1] });
                    }

                    Dictionary<string, List<string>> map_fix = f.fixListName();

                    foreach (string k in map_fix.Keys)
                    {
                        new_map.Add(k, 0);

                    }
                    foreach (string n in f.map_name.Keys)
                    {
                        if (new_map.ContainsKey(n))
                        {
                            new_map[n] += f.map_name.GetValueOrDefault(n);
                        }
                        else if (f.lst_dupNames.Contains(n))
                        {
                            foreach (string k in map_fix.Keys)
                            {
                                if (map_fix.GetValueOrDefault(k).Contains(n))
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
                        if (new_map.GetValueOrDefault(k) != 0)
                            Console.WriteLine(k + ' ' + new_map.GetValueOrDefault(k));
                    }
                    work = 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: " + e);
                    break;
                }
            }
        }
    }
}
