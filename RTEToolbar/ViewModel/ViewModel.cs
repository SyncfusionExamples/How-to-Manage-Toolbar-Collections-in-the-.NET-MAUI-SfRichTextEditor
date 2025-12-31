using Syncfusion.Maui.RichTextEditor;
using System.ComponentModel;

namespace RTEToolbar
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _mailContent = string.Empty;

        private RichTextEditorToolBarItemCollection toolbarItems = new();

        public RichTextEditorToolBarItemCollection ToolbarItems
        {
            get
            {
                return toolbarItems;
            }
            set
            {
                toolbarItems = value;
                OnPropertyChanged(nameof(ToolbarItems));
            }
        }

        /// <summary>
        /// Gets or sets the content of the mail.
        /// </summary>
        public string MailContent
        {
            get => _mailContent;
            set
            {
                _mailContent = value;
                OnPropertyChanged(nameof(MailContent));
            }
        }

        public ViewModel()
        {
            ToolbarItems = new RichTextEditorToolBarItemCollection();
            // Initialize default toolbar items
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Bold });
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Italic });
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Underline });
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Separator });
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.NumberList });
            ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.BulletList });

            MailContent = @"<p><strong>Hello Frank,</strong></p><p>I hope you're doing well. I'm writing to follow up on our recent conversation regarding <strong>MAUI control development</strong>.</p><p>Here’s a quick summary of the agenda:</p><ul><li>✅ Kickoff completed successfully</li><li>📌 Deliverables in progress</li><li>💬 Weekly sync scheduled</li></ul><p><strong>Detailed Task Overview:</strong></p><table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse;'><tr><th>Task</th><th>Owner</th><th>Due Date</th><th>Status</th></tr><tr><td>Kickoff Meeting</td><td>Project Lead</td><td>August 19</td><td>✅ Completed</td></tr><tr><td>Deliverables Review</td><td>Dev Team</td><td>September 30</td><td>🔄 In Progress</td></tr><tr><td>Weekly Sync</td><td>All Members</td><td>Every Tuesday</td><td>📅 Scheduled</td></tr></table><p style='margin-top:20px; margin-bottom:20px;'>If you need more details, feel free to reach out. I’ll be happy to provide any additional information you need.</p><p>Best regards,<br/><strong>Ivy</strong><br/>Technical Project Coordinator</p>";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event.
        /// </summary>
        /// <param name="name">The property name that changed.</param>
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
