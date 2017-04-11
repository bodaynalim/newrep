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
            GetPathOfAssemlies(fileAssemblyList,StrGacDir);
            GetPathOfAssemlies(fileAssemblyList, StrGacDir2);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools";
            startInfo.Arguments = @"/c gacutil /l";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            
            process.Start();

            List<string> lines = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                 lines.Add(process.StandardOutput.ReadLine());
               
            }

            List<string> infoAboutAssemblies = new List<string>();

            for (int i = 4; i < lines.Count - 2; i++)
            {
                infoAboutAssemblies.Add(lines[i]);
                
            }



            foreach (var assembly in infoAboutAssemblies)
            {
                
                string[] infoAssemly = assembly.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> assemblyParameters = new Dictionary<string, string>();

                for (int i = 1; i < infoAssemly.Length; i++)
                {
                    string[] parameter = infoAssemly[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    parameter[0] = parameter[0].Replace(" ", string.Empty);
                    assemblyParameters.Add(parameter[0], parameter[1]);
                   
                }
                infoAssemly[0] = infoAssemly[0].Replace(" ", string.Empty);
                string assemblyPath=null;
                List<string> PathtoAssemb = new List<string>();

                foreach (var pathToAssembly in fileAssemblyList)
                {
                    if(pathToAssembly.EndsWith(infoAssemly[0]+".dll") || pathToAssembly.EndsWith(infoAssemly[0] + ".exe"))
                        
                        PathtoAssemb.Add(pathToAssembly);
                }

                foreach (var pathToAssembly in PathtoAssemb)
                {
                    if (assemblyParameters["Culture"] != "neutral")
                    {
                        if (pathToAssembly.Contains(assemblyParameters["Version"]) &&
                            pathToAssembly.Contains(assemblyParameters["Culture"]) && pathToAssembly.Contains(assemblyParameters["PublicKeyToken"]))
                        {
                            assemblyPath = pathToAssembly;
                         

                        }
                    }
                    else
                    {
                        if (pathToAssembly.Contains(assemblyParameters["Version"]) &&
                            pathToAssembly.Contains(assemblyParameters["PublicKeyToken"]))
                        {
                            assemblyPath = pathToAssembly;
                          

                        }
                    }
                }


                if (assemblyPath != null)
                {
                    assemliesActions.Add(new GacAction()
                    {
                        Name = infoAssemly[0],
                        Path = assemblyPath,
                        Parameters = assemblyParameters
                    });
                }
            }
          

         

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
