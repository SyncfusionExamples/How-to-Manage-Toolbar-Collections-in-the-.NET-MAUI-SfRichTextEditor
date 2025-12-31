using Syncfusion.Maui.RichTextEditor;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RTEToolbar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdateItemCount();
            UpdatePickerItems();
        }

        /// <summary>
        /// Handles checkbox state changes to add or remove toolbar items
        /// </summary>
        private void OnToolbarItemCheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (sender is not CheckBox checkBox)
                return;

            // Determine which item type based on checkbox
            RichTextToolbarOptions itemType = checkBox.StyleId switch
            {
                _ when checkBox == chkStrikethrough => RichTextToolbarOptions.Strikethrough,
                _ when checkBox == chkSubScript => RichTextToolbarOptions.SubScript,
                _ when checkBox == chkSuperScript => RichTextToolbarOptions.SuperScript,
                _ when checkBox == chkFontFamily => RichTextToolbarOptions.FontFamily,
                _ when checkBox == chkFontSize => RichTextToolbarOptions.FontSize,
                _ when checkBox == chkTextColor => RichTextToolbarOptions.TextColor,
                _ when checkBox == chkHighlightColor => RichTextToolbarOptions.HighlightColor,
                _ when checkBox == chkAlignment => RichTextToolbarOptions.Alignment,
                _ when checkBox == chkIncreaseIndent => RichTextToolbarOptions.IncreaseIndent,
                _ when checkBox == chkDecreaseIndent => RichTextToolbarOptions.DecreaseIndent,
                _ when checkBox == chkHyperlink => RichTextToolbarOptions.Hyperlink,
                _ when checkBox == chkImage => RichTextToolbarOptions.Image,
                _ when checkBox == chkTable => RichTextToolbarOptions.Table,
                _ => RichTextToolbarOptions.Bold
            };

            if (e.Value)
            {
                // Add item if checked
                if (checkBox == chkUndoRedo)
                {
                    // Add both Undo and Redo
                    AddToolbarItem(RichTextToolbarOptions.Undo);
                    AddToolbarItem(RichTextToolbarOptions.Redo);
                }
                else
                {
                    AddToolbarItem(itemType);
                }
            }
            else
            {
                // Remove item if unchecked
                if (checkBox == chkUndoRedo)
                {
                    // Remove both Undo and Redo
                    RemoveToolbarItem(RichTextToolbarOptions.Undo);
                    RemoveToolbarItem(RichTextToolbarOptions.Redo);
                }
                else
                {
                    RemoveToolbarItem(itemType);
                }
            }

            UpdateItemCount();
            UpdatePickerItems();
        }

        /// <summary>
        /// Adds a toolbar item to the collection
        /// </summary>
        private void AddToolbarItem(RichTextToolbarOptions itemType)
        {
            // Check if item already exists
            if (!richTextEditor.ToolbarItems.Any(item => item.Type == itemType))
            {
                richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = itemType });
            }
        }

        /// <summary>
        /// Removes a toolbar item from the collection
        /// </summary>
        private void RemoveToolbarItem(RichTextToolbarOptions itemType)
        {
            var itemToRemove = richTextEditor.ToolbarItems.FirstOrDefault(item => item.Type == itemType);
            if (itemToRemove != null)
            {
                richTextEditor.ToolbarItems.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Clears all toolbar items
        /// </summary>
        private void OnClearAllClicked(object? sender, EventArgs e)
        {
            richTextEditor.ToolbarItems.Clear();
            
            // Uncheck all checkboxes
            chkStrikethrough.IsChecked = false;
            chkSubScript.IsChecked = false;
            chkSuperScript.IsChecked = false;
            chkFontFamily.IsChecked = false;
            chkFontSize.IsChecked = false;
            chkTextColor.IsChecked = false;
            chkHighlightColor.IsChecked = false;
            chkAlignment.IsChecked = false;
            chkIncreaseIndent.IsChecked = false;
            chkDecreaseIndent.IsChecked = false;
            chkHyperlink.IsChecked = false;
            chkImage.IsChecked = false;
            chkTable.IsChecked = false;
            chkUndoRedo.IsChecked = false;

            UpdateItemCount();
            UpdatePickerItems();
        }

        /// <summary>
        /// Resets toolbar to default items
        /// </summary>
        private void OnResetToDefaultClicked(object? sender, EventArgs e)
        {
            richTextEditor.ToolbarItems.Clear();
            
            // Add default items
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Bold });
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Italic });
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Underline });
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Separator });
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.NumberList });
            richTextEditor.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.BulletList });

            // Reset checkboxes
            chkStrikethrough.IsChecked = false;
            chkSubScript.IsChecked = false;
            chkSuperScript.IsChecked = false;
            chkFontFamily.IsChecked = false;
            chkFontSize.IsChecked = false;
            chkTextColor.IsChecked = false;
            chkHighlightColor.IsChecked = false;
            chkAlignment.IsChecked = false;
            chkIncreaseIndent.IsChecked = false;
            chkDecreaseIndent.IsChecked = false;
            chkHyperlink.IsChecked = false;
            chkImage.IsChecked = false;
            chkTable.IsChecked = false;
            chkUndoRedo.IsChecked = false;

            UpdateItemCount();
            UpdatePickerItems();
        }

        /// <summary>
        /// Updates the item count label
        /// </summary>
        private void UpdateItemCount()
        {
            lblItemCount.Text = $"Current Item Count: {richTextEditor.ToolbarItems.Count}";
        }

        /// <summary>
        /// Updates the picker with current toolbar items
        /// </summary>
        private void UpdatePickerItems()
        {
            pickerToolbarItems.Items.Clear();
            
            for (int i = 0; i < richTextEditor.ToolbarItems.Count; i++)
            {
                var item = richTextEditor.ToolbarItems[i];
                pickerToolbarItems.Items.Add($"{i + 1}. {item.Type}");
            }

            if (pickerToolbarItems.Items.Count > 0)
            {
                pickerToolbarItems.SelectedIndex = -1;
            }

            btnMoveLeft.IsEnabled = false;
            btnMoveRight.IsEnabled = false;
        }

        /// <summary>
        /// Handles picker selection change
        /// </summary>
        private void OnPickerSelectedIndexChanged(object? sender, EventArgs e)
        {
            if (pickerToolbarItems.SelectedIndex >= 0)
            {
                // Enable/disable move buttons based on position
                btnMoveLeft.IsEnabled = pickerToolbarItems.SelectedIndex > 0;
                btnMoveRight.IsEnabled = pickerToolbarItems.SelectedIndex < richTextEditor.ToolbarItems.Count - 1;
            }
            else
            {
                btnMoveLeft.IsEnabled = false;
                btnMoveRight.IsEnabled = false;
            }
        }

        /// <summary>
        /// Moves selected item to the left (decreases index)
        /// </summary>
        private void OnMoveLeftClicked(object? sender, EventArgs e)
        {
            int selectedIndex = pickerToolbarItems.SelectedIndex;
            
            if (selectedIndex > 0)
            {
                var item = richTextEditor.ToolbarItems[selectedIndex];
                richTextEditor.ToolbarItems.RemoveAt(selectedIndex);
                richTextEditor.ToolbarItems.Insert(selectedIndex - 1, item);
                
                UpdatePickerItems();
                pickerToolbarItems.SelectedIndex = selectedIndex - 1;
            }
        }

        /// <summary>
        /// Moves selected item to the right (increases index)
        /// </summary>
        private void OnMoveRightClicked(object? sender, EventArgs e)
        {
            int selectedIndex = pickerToolbarItems.SelectedIndex;
            
            if (selectedIndex >= 0 && selectedIndex < richTextEditor.ToolbarItems.Count - 1)
            {
                var item = richTextEditor.ToolbarItems[selectedIndex];
                richTextEditor.ToolbarItems.RemoveAt(selectedIndex);
                richTextEditor.ToolbarItems.Insert(selectedIndex + 1, item);
                
                UpdatePickerItems();
                pickerToolbarItems.SelectedIndex = selectedIndex + 1;
            }
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string _mailContent = string.Empty;
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
