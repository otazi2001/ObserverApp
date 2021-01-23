using System;
using System.Collections.Generic;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class NotesManager
    {
        private string reportNotesPath;
        private List<string> notesLines;

        public string Notes
        {
            get
            {
                var output = "";
                foreach (string line in notesLines)
                {
                    output += $"\n{line}";
                }
                return output;
            }
            set
            {
                notesLines = new List<string>(value.Split('\n'));
            }
        }

        public NotesManager(string path)
        {
            reportNotesPath = path;
            notesLines = new List<string>(File.ReadAllLines(reportNotesPath));
        }

        public void SaveNotes()
        {
            File.WriteAllLines(reportNotesPath, notesLines);
        }
    }
}
