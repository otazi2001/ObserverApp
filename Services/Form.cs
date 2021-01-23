using System;
using System.Collections.Generic;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class Form
    {

        private string reportFormPath;

        private List<Section> sections = new List<Section>();

        public List<Section> Sections
        {
            get
            {
                return sections;
            }
        }

        public Form(string[] lines)
        {
            BuildForm(lines);
        }

        public Form(string reportFormPath)
        {
            this.reportFormPath = reportFormPath;

            BuildForm(File.ReadAllLines(reportFormPath));
        }

        public void removeSection(Section s)
        {
            sections.Remove(s);
        }

        public void addSection(Section s)
        {
            sections.Add(s);
        }

        private void BuildForm(string[] formLines)
        {
            List<Section> list = new List<Section>();

            for(var i = 0; i < formLines.Length; i++)
            {
                string[] info = formLines[i].Split(',');
                if(info[0] == "Section")
                {
                    list.Add(new Section(info[1]));
                }
                else
                {
                    var qType = getType(info[0]);
                    list[list.Count - 1].addQuestion(new Question(qType,info[1],info[2],info[3].Split('\t')));
                }
            }

            this.sections = list;

        }

        private QuestionType getType(string testStr)
        {
            switch (testStr)
            {
                case "Multiple":
                    return QuestionType.Multiple;
                case "Text":
                    return QuestionType.Text;
                default:
                    return QuestionType.None;
            }

        }

        public void SaveForm()
        {
            File.WriteAllText(reportFormPath, string.Empty);
            foreach(Section item in Sections)
            {
                File.AppendAllLines(reportFormPath, item.SectionToCSV());
            }
        }


    }
}
