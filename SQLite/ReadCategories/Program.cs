using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP;
using EP.Semantix;
using EP.Text;
using LingvoNET;


namespace ReadCategories
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor processor = new Processor();
            EP.Text.Morphology.UnloadLanguages(MorphLang.UA);
            Console.OutputEncoding = Encoding.UTF8;
            var categories = File.ReadAllLines(@"C:\Users\pervolo\Documents\newrep\SQLite\Result\Категории.txt", Encoding.UTF8);
            foreach (var category in categories)
            {
                AnalysisResult result = processor.Process(new SourceOfAnalysis("закон"));
                string cat = "";
               
               

                Console.WriteLine(result.FirstToken.GetNormalCaseText(singleNumber: true, gender: MorphGender.Masculine));
                cat = null;
            }









            Console.ReadLine();
        }
    }
}
