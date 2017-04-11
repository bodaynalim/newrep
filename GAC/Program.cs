using Advanced.System.Monitoring.Models.Actions.GacActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GAC
{
    class Program
    {
        public static readonly string StrGacDir = "C:\\Windows\\Microsoft.NET\\assembly\\GAC_MSIL";
        public static readonly string StrGacDir2 = @"C:\Windows\assembly\GAC_MSIL";

        static void Main(string[] args)
        {
            List<GacAction> assemliesActions = new List<GacAction>();
            List<string> fileAssemblyList = new List<string>();

            GetPathOfAssemlies(fileAssemblyList, StrGacDir);
            GetPathOfAssemlies(fileAssemblyList, StrGacDir2);
           
            foreach (var file in fileAssemblyList)
            {
                var assam = Assembly.LoadFile(file);
                string[] infoAssemly = assam.FullName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> assemblyParameters = new Dictionary<string, string>();

                for (int i = 1; i < infoAssemly.Length; i++)
                {
                    string[] parameter = infoAssemly[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    assemblyParameters.Add(parameter[0], parameter[1]);

                }

                Console.WriteLine(infoAssemly[0] + " " + assam.Location);
                assemliesActions.Add(new GacAction()
                {
                    Name = infoAssemly[0],
                    Path = file,
                    Parameters = assemblyParameters
                });
              
            }
          
            Console.ReadLine();

        }

        public static void GetPathOfAssemlies(List<string> fileAssemblyList, string StrGacDir)
        {
            string[] subDirs1 = Directory.GetDirectories(StrGacDir);

            foreach (string subDir1 in subDirs1)
            {
                var subDirs2 = Directory.GetDirectories(subDir1);

                foreach (string subDir2 in subDirs2)
                {
                    var strFiles = Directory.GetFiles(subDir2, "*.dll");
                    var strFilesExe = Directory.GetFiles(subDir2, "*.exe");
                    fileAssemblyList.AddRange(strFiles);
                    fileAssemblyList.AddRange(strFilesExe);
                }
            }
        }
    }
}
