using System;
using System.Collections.Generic;

namespace ObseverAppCW2.Services
{
    public class Section
    {

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private List<Question> questions = new List<Question>();

        public List<Question> Questions
        {
            get
            {
                return questions;
            }
        }

        public Section(string name)
        {
            this.name = name;
        }

        public void addQuestion(Question newQuestion)
        {
            
            questions.Add(newQuestion);
        }

        public void removeQuestion(Question selectedQuestion)
        {
            questions.Remove(selectedQuestion);
        }

        public string[] SectionToCSV()
        {
            string[] lines = new string[Questions.Count+1];
            lines[0] = $"Section,{Name}";

            for(var i = 1; i <= Questions.Count; ++i)
            {
                lines[i] = Questions[i - 1].QuestionToCSV();
            }

            return lines;
        }
    }
}
