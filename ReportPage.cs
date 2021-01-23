using System;

using Xamarin.Forms;

namespace ObseverAppCW2
{
    public class ReportPage : TabbedPage
    {
        public ReportPage(Services.Report report)
        {
            Children.Add(new ReportFormPage(report.ReportForm) {
                Title = "Form",
                IconImageSource = "FormIcon.png"
            });;
            Children.Add(new ReportNotesPage(report.ReportNotes) {
                Title = "Notes",
                IconImageSource = "NoteIcon.png"
            });
            Children.Add(new ReportParticipantsPage(report.ReportPartipants) {
                Title = "Participants",
                IconImageSource = "PartIcon.png"
            });
        }
    }
}

