using System;
namespace ObseverAppCW2.Services
{
    public class Participant
    {

        private string name = "Empty404";
        private string birthday = "Empty404";
        private string gender = "Empty404";
        private string role = "Empty404";
        private string info = "Empty404";

        public string Name
        {
            get { if (name == "Empty404") { return ""; } else { return name; } }
            set { if (value == "") { name = "Empty404"; } else { name = value; } }
        }

        public string Birthday
        {
            get { if ( birthday == "Empty404") { return ""; } else { return birthday; } }
            set { if (value == "") {  birthday = "Empty404"; } else { birthday = value; } }
        }

        public string Gender
        {
            get { if ( gender == "Empty404") { return ""; } else { return gender; } }
            set { if (value == "") {  gender = "Empty404"; } else {  gender = value; } }
        }

        public string Role
        {
            get { if ( role == "Empty404") { return ""; } else { return role; } }
            set { if (value == "") {  role = "Empty404"; } else {  role = value; } }
        }

        public string Info
        {
            get { if ( info == "Empty404") { return ""; } else { return info; } }
            set { if (value == "") { info = "Empty404"; } else { info = value; } }
        }

        public Participant()
        {
        }

        public Participant(string n, string b, string g, string r, string i)
        {
            name = n;
            birthday = b;
            gender = g;
            role = r;
            info = i;
        }

        public string ParticipantToCSV()
        {
            return $"{Name},{Birthday},{Gender},{Role},{Info}";
        }
    }
}
