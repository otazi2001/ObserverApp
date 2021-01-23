using System;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class DefaultReportForm
    {

        private string[] customlines = new string[0];

        public string[] Lines
        {
            get
            {
                if(customlines.Length > 0)
                {
                    return customlines;
                }
                else
                {
                    return new string[]{
                        "Section,General",
                        "Text,Where is the trial taking place?,Empty404,Empty404\tEmpty404",
                        "Text,How many juries will be present?,Empty404,Empty404\tEmpty404",
                        "Text,What are the crimes the defendant is being accused of?,Empty404,Empty404\tEmpty404",
                        "Section,Conditions",
                        "Text,How many hours was the defendant given to prepare?,Empty404,Empty404\tEmpty404",
                        "Multiple,Is the trial being held Publicly?,Empty404,Yes\tNo"
                    };
                }
            }
        }

        public DefaultReportForm()
        {
            var MyDocs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "customObserverForm.txt");
            if (File.Exists(MyDocs))
            {
                customlines = File.ReadAllLines(MyDocs);
            }

        }

        public void NewCustomForm(Form form)
        {
            var MyDocs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "customObserverForm.txt");

            File.WriteAllText(MyDocs, string.Empty);
            foreach (Section item in form.Sections)
            {
                File.AppendAllLines(MyDocs, item.SectionToCSV());
            }
        }

        public void resetForm()
        {
            var MyDocs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "customObserverForm.txt");
            System.IO.File.Delete(MyDocs);
        }




    }
}
