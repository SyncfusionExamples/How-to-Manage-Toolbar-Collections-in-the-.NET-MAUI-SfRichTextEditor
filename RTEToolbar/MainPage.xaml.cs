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
            if (!VM.ToolbarItems.Any(item => item.Type == itemType))
            {
                VM.ToolbarItems.Add(new RichTextToolbarItem { Type = itemType });
            }
        }

        /// <summary>
        /// Removes a toolbar item from the collection
        /// </summary>
        private void RemoveToolbarItem(RichTextToolbarOptions itemType)
        {
            var itemToRemove = VM.ToolbarItems.FirstOrDefault(item => item.Type == itemType);
            if (itemToRemove != null)
            {
                VM.ToolbarItems.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Clears all toolbar items
        /// </summary>
        private void OnClearAllClicked(object? sender, EventArgs e)
        {
            VM.ToolbarItems.Clear();
            
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
            VM.ToolbarItems.Clear();
            
            // Add default items
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Bold });
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Italic });
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Underline });
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.Separator });
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.NumberList });
            VM.ToolbarItems.Add(new RichTextToolbarItem { Type = RichTextToolbarOptions.BulletList });

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
            lblItemCount.Text = $"Current Item Count: {VM.ToolbarItems.Count}";
        }

        /// <summary>
        /// Updates the picker with current toolbar items
        /// </summary>
        private void UpdatePickerItems()
        {
            pickerToolbarItems.Items.Clear();
            
            for (int i = 0; i < VM.ToolbarItems.Count; i++)
            {
                var item = VM.ToolbarItems[i];
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
                btnMoveRight.IsEnabled = pickerToolbarItems.SelectedIndex < VM.ToolbarItems.Count - 1;
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
                var item = VM.ToolbarItems[selectedIndex];
                VM.ToolbarItems.RemoveAt(selectedIndex);
                VM.ToolbarItems.Insert(selectedIndex - 1, item);
                
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
            
            if (selectedIndex >= 0 && selectedIndex < VM.ToolbarItems.Count - 1)
            {
                var item = VM.ToolbarItems[selectedIndex];
                VM.ToolbarItems.RemoveAt(selectedIndex);
                VM.ToolbarItems.Insert(selectedIndex + 1, item);
                
                UpdatePickerItems();
                pickerToolbarItems.SelectedIndex = selectedIndex + 1;
            }
        }
    }
}
