using System;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class Report
    {
        public string Name { get;  private set; }

        public string ReportPath  {  get {return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Name); } }

        public string ReportForm { get { return Path.Combine(ReportPath, "ReportForm.txt"); } }

        public string ReportPartipants { get  { return Path.Combine(ReportPath, "ReportParticipants.txt"); } }

        public string ReportNotes { get { return Path.Combine(ReportPath, "ReportNotes.txt"); } }


        public Report(string name)
        {
            this.Name = name;
        }

    }


}
