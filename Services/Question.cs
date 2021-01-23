using System;
using System.Collections.Generic;

namespace ObseverAppCW2.Services
{
    public class Question
    {

        private bool editable = false;

        private string instruction = "Empty404";

        private string answer = "Empty404";

        private List<string> options = new List<string>();

        public QuestionType Type
        {
            get;
            private set;
        }

        public string Instruction
        {
            get
            {
                if(instruction == "Empty404")
                {
                    return "";
                }
                else
                {
                    return instruction;
                }
                
            }
            set
            {
                if (editable)
                {
                    if (value == "")
                    {
                        instruction = "Empty404";
                    }
                    else
                    {
                        instruction = value;
                    }
                    
                }
            }
        }

        public string Answer
        {
            get
            {
                if (answer == "Empty404") { return ""; }
                else { return answer; }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    answer = "Empty404";
                }
                else
                {
                    answer = value;
                }

            }
        }

        public List<string> Options
        {
            get
            {
                List<string> list = new List<string>();
                foreach (string item in options)
                {
                    if (item != "Empty404")
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            set
            {
                if (editable)
                {
                    options = value;
                }
            }

        }

        public Question(QuestionType InType)
        {
            this.editable = true;
            this.Type = InType;
        }


        public Question(QuestionType InType, string InInstruction, string InAnswer, string[] InOptions)
        {
            this.answer = InAnswer;
            this.Type = InType;
            this.instruction = InInstruction;
            this.options = new List<string>(InOptions);
        }

        public string QuestionToCSV()
        {
            var ops = "";
            if (Options.Count < 1)
            {
                ops += "Empty\t";
            }

            foreach (string item in Options)
            {
                ops += $"{item}\t";
            }

            return $"{Type.ToString()},{Instruction},{Answer},{ops}";
        }
    }
}
