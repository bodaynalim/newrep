using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LingvoNET;
using EP;
using EP.Semantix;
using EP.Text;

namespace SQLite
{
    class Program
    {
       
        static void Main(string[] args)
        {
            if(File.Exists(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\ПодкатегорииNORMILIZED.txt"))
                File.Delete(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\ПодкатегорииNORMILIZED.txt");

            if(File.Exists(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\КатегорииNORMILIZED.txt"))
                File.Delete(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\КатегорииNORMILIZED.txt");

            
            Console.OutputEncoding = Encoding.UTF8;
            EP.Text.Morphology.UnloadLanguages(MorphLang.UA);
            var db = new SQLiteConnection(@"Data Source=C:\Users\pervolo\Documents\newrep\SQLite\SQLite\bin\Debug\911.db;", true);
            Processor processor = new Processor();
            db.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT Distinct(`category_name`) FROM `911_questions`;", db);
            SQLiteDataReader reader = command.ExecuteReader();

            List<string> records = new List<string>();
            var stopWord =
                File.ReadAllLines(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\Стоп-слова.txt");

            foreach (DbDataRecord record in reader)
            {
                List<string> subrecords = new List<string>();

                List<string> recordssub = new List<string>();

                SQLiteCommand subcommand =
                    new SQLiteCommand("SELECT Distinct(`subcategory_name`) FROM `911_questions` WHEre `category_name`='" + record["category_name"]+"';", db);
                SQLiteDataReader subreader = subcommand.ExecuteReader();

                AnalysisResult result = processor.Process(new SourceOfAnalysis(record["category_name"].ToString()));
                string rec = "";
                for (Token t = result.FirstToken; t != null; t = t.Next)
                {
                   if ((t.Morph.Class.IsNoun || t.Morph.Class.IsAdjective || t.IsReferent ) 
                       && !t.Morph.Class.IsPreposition && stopWord.All(x =>
                            !x.EndsWith(t.GetNormalCaseText(singleNumber: true, gender: MorphGender.Masculine), StringComparison.InvariantCultureIgnoreCase)
                                 && !x.StartsWith(t.GetNormalCaseText(singleNumber: true, gender: MorphGender.Masculine), StringComparison.InvariantCultureIgnoreCase)))
                   rec += t.GetNormalCaseText(gender: MorphGender.Masculine) + " ";
                }
                
                recordssub.Add(rec.ToLowerInvariant());
                File.AppendAllLines(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\ПодкатегорииNORMILIZED.txt",recordssub);

                records.Add(rec.ToLowerInvariant());

                foreach (DbDataRecord subrecord in subreader)
                {
                    result = processor.Process(new SourceOfAnalysis(subrecord["subcategory_name"].ToString()));
                    string cat = "";
                    for (Token t = result.FirstToken; t != null; t = t.Next)
                    {
                        if ((t.Morph.Class.IsNoun || t.Morph.Class.IsAdjective || t.IsReferent )
                            && !t.Morph.Class.IsPreposition && stopWord.All(x=> 
                            !x.EndsWith(t.GetNormalCaseText(singleNumber: true, gender: MorphGender.Masculine), StringComparison.InvariantCultureIgnoreCase) 
                                 && !x.StartsWith(t.GetNormalCaseText(singleNumber: true, gender: MorphGender.Masculine), StringComparison.InvariantCultureIgnoreCase)))
                            cat += t.GetNormalCaseText(gender: MorphGender.Masculine) + " ";
                    }
                    cat = cat.ToLowerInvariant();
                    if (cat.Contains("авт "))
                    {
                        cat = cat.Replace("авт ", "авто ");
                     }
                    if (cat.Contains("деньга"))
                    {
                        cat = cat.Replace("деньга", "деньги");
                    }
                    string ct = "";
                    foreach (var x in cat)
                    {
                       if (!char.IsDigit(x))
                            {
                                ct += x;
                            }

                    }

                    subrecords.Add("\t\t" + ct.TrimStart());
                    subrecords =   subrecords.Where(x=> !string.IsNullOrWhiteSpace(x)).ToList();

                }
                var j = subrecords.GroupBy(x => x).Select(x => x.First()).ToList();
               AddToFIle(@"C:\Users\pervolo\Documents\newrep\SQLite\Result", @"\ПодкатегорииNORMILIZED", j);   
            }

            
            AddToFIle(@"C:\Users\pervolo\Documents\newrep\SQLite\Result", @"\КатегорииNORMILIZED", records);
            
            db.Close();

            Console.ReadLine();
        }

        public static void AddToFIle(string path, string nameFile, List<string> records  )
        {
            if (Directory.Exists(@path))
            {
                File.AppendAllLines(@path + nameFile + ".txt",
                    records);
            }
            else
            {
                Directory.CreateDirectory(@path);
                File.WriteAllLines(@path + nameFile + ".txt",
                   records);
            }

        }
    }
}
