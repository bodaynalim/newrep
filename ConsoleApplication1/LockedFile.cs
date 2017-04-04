using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class LockedFile
    {
        public LockedFile(string path, List<Process> getProcessById)
        {
            ChangedFile = path;
            Processes = getProcessById;
        }

        public string ChangedFile { get; set; }
        public List<Process> Processes { get; set; }

    }
}
