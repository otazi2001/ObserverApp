using System;
using System.Collections.Generic;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class ReportManager
    {
        private string DocumentsPath= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public List<DirectoryInfo> ReportNames
        {
            get
            {
                DirectoryInfo AppDirectory = new DirectoryInfo(DocumentsPath);
                var names = new List<DirectoryInfo>();
                foreach (DirectoryInfo item in AppDirectory.GetDirectories())
                {
                    if(item.Name == ".config") { continue; }
                    names.Add(item);
                }
                return names;
            }
        }

        public ReportManager() { }

        public void CreateNewReport(string name)
        {
            var path = Path.Combine(DocumentsPath, name);
            if (!Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                DefaultReportForm defRepForm = new DefaultReportForm();
                File.WriteAllLines(Path.Combine(path,"ReportForm.txt"), defRepForm.Lines);
                System.IO.File.Create(Path.Combine(path, "ReportParticipants.txt"));
                System.IO.File.Create(Path.Combine(path, "ReportNotes.txt"));
                //return true;
            }
        }

        public void DeleteReport(string name)
        {
            var path = Path.Combine(DocumentsPath, name);
            if (Directory.Exists(path))
            {
                DirectoryInfo reportDirectory = new DirectoryInfo(path);
                foreach(FileInfo item in reportDirectory.GetFiles())
                {
                    System.IO.File.Delete(item.FullName);
                }
                System.IO.Directory.Delete(path);
                //return true;

            }
        }


    }
}
