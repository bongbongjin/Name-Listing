using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
    class Program
    {
        static List<string> names = new List<string>();
        static List<NameInfo> nameinfos = new List<NameInfo>();
        static public void readNames()
        {
            List<string> data = LoadFile("names.txt");

            for (int i = 0; i < data.Count; i++)
            {
                try
                {
                    string line = data[i];
                    string[] stringSeparators = new string[] { "," };
                    string[] result = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < result.Count(); j++)
                        names.Add(result[j]);
                }
                catch (Exception ex)
                {

                }
            }
            names = names.Select(n => n.Replace("\"", "")).ToList();
        }
        static public void populateNameInfos()
        {
            string name = "";
            for (int i = 0; i < names.Count; i ++)
            {
                name = names[i];
                NameInfo info = new NameInfo();
                info.name = names[i];
                for (int j = 0; j < name.Count(); j++)
                {
                    char ch = name[j];
                    int chWeight = (int)(ch - 'A') + 1;
                    info.weight += chWeight;
                }
                nameinfos.Add(info);
            }
            nameinfos = nameinfos.OrderBy(n => n.weight).ToList();
            
        }

        static public List<string> LoadFile(string filename)
        {
            var importfile = File.ReadAllLines(filename);
            List<string> importData = new List<string>(importfile);
            return importData;
        }
        static void Main(string[] args)
        {
            readNames();
            populateNameInfos();
            int totalScore = 0;
            for (int i = 0; i < nameinfos.Count; i++)
            {
                int score = i * nameinfos[i].weight;
                Console.WriteLine("{0} {1} {2}", i,  nameinfos[i].name, score);
                totalScore += score;
            }
            Console.WriteLine("Total Score {0}", totalScore);
        }

        class NameInfo
        {
            public int weight { get; set; }
            public string name { get; set; }
            public int score { get; set; }
        }
    }
}
