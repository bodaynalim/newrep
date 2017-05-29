using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoDetect
{
    class Program
    {
        static void Main(string[] args)
        {
            string undetect = @"C:\Users\pervolo\Desktop\SNFaceCrop\SNFaceCrop\bin\Release\foto";
            string detect = @"C:\Users\pervolo\Desktop\SNFaceCrop\SNFaceCrop\bin\Release\foto\detected faces";
            string faceundetect = @"C:\Users\pervolo\Desktop\SNFaceCrop\SNFaceCrop\bin\Release\foto\undetected faces";
            var face = Directory.GetFiles(detect);
            var files=Directory.GetFiles(undetect);
             List<string> detectedFaces = new List<string>();
            List<string> undetectedFaces = new List<string>();
            List<string> undetectedFacesCompare = new List<string>();
            List<string> undetectedFacesComparePath = new List<string>();

            foreach (var file in face)
            {
              var splitFace = Path.GetFileNameWithoutExtension(file).Split(new char[] {'_'}, StringSplitOptions.RemoveEmptyEntries);
                detectedFaces.Add(splitFace[0]);

            }
            foreach (var file in files)
            {
                undetectedFaces.Add(Path.GetFileNameWithoutExtension(file));
            }

            undetectedFacesCompare.AddRange(undetectedFaces.Where(x=> detectedFaces.All(y=>!x.Equals(y))));
            undetectedFacesComparePath.AddRange(files.Where(x=> undetectedFacesCompare.Any(y=> Path.GetFileNameWithoutExtension(x).Equals(y))));
            Console.WriteLine(undetectedFacesComparePath.Count);
            foreach (var file in undetectedFacesComparePath)
            {
                
                File.Copy(file, Path.Combine(faceundetect+"\\",Path.GetFileName(file)));
            }
            Console.ReadLine();
        }
    }
}
