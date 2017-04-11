using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var db = new SQLiteConnection(@"Data Source=C:\Users\pervolo\Documents\newrep\SQLite\SQLite\bin\Debug\911.db;", true);

            db.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT Distinct(`category_name`) FROM `911_questions`;", db);
            SQLiteDataReader reader = command.ExecuteReader();

            List<string> records = new List<string>();
            List<string> subrecords = new List<string>();

            foreach (DbDataRecord record in reader)
            {

                records.Add(record["category_name"].ToString());
                Console.WriteLine("Категория: "+record["category_name"]);
                SQLiteCommand subcommand =
                    new SQLiteCommand("SELECT Distinct(`subcategory_name`) FROM `911_questions` WHEre `category_name`='" + record["category_name"]+"';", db);
                SQLiteDataReader subreader = subcommand.ExecuteReader();

             
                subrecords.Add(record["category_name"].ToString());
                foreach (DbDataRecord subrecord in subreader)
                {
                    subrecords.Add("\t\t" + subrecord["subcategory_name"]);

                }
                
             
            }
            AddToFIle(@"C:\Users\pervolo\Documents\newrep\SQLite\Result", @"\Подкатегории",subrecords);
            AddToFIle(@"C:\Users\pervolo\Documents\newrep\SQLite\Result", @"\Категории" , records);
            
            db.Close();
        }

        public static void AddToFIle(string path, string nameFile, List<string> records )
        {
            if (Directory.Exists(@path))
            {
                File.WriteAllLines(@path+nameFile+".txt",
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
