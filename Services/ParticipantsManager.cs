using System;
using System.Collections.Generic;
using System.IO;

namespace ObseverAppCW2.Services
{
    public class ParticipantsManager
    {

        private string reportParticipantsPath;

        private List<Participant> participants;

        public List<Participant> Participants { get { return participants; } }

        public ParticipantsManager(string path)
        {
            this.reportParticipantsPath = path;
            readPartipants(File.ReadAllLines(path));
        }

        public void AddPartipant(Participant p)
        {
            participants.Add(p);
        }

        public void RemovePartipant(Participant p)
        {
            participants.Remove(p);
        }

        private void readPartipants(string[] participantLines)
        {
            List<Participant> list = new List<Participant>();

            foreach(string item in participantLines)
            {
                var p = item.Split(',');
                list.Add(new Participant(p[0], p[1], p[2], p[3], p[4]));
            }

            participants = list;
        }

        public void SaveParticipants()
        { 
            string[] lines = new string[participants.Count];
            for (var i = 0; i < participants.Count; i++)
            {
                lines[i] = participants[i].ParticipantToCSV();
            }
            File.WriteAllLines(reportParticipantsPath, lines);
        }
    }
}
