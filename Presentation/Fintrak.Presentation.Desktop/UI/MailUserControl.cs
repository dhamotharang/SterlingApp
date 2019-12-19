using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;
using Fintrak.Presentation.Desktop.Properties;
using Telerik.WinControls.UI.Docking;
using System.Windows.Forms;

namespace Fintrak.Presentation.Desktop.UI
{
    public partial class MailUserControl : UserControl
    {
        private Font boldFont;
        private FilterDescriptor unreadFilter;

        public MailUserControl()
        {
            InitializeComponent();

            PopulateBackstageView();

            ModifyRibbon();

            BindTreeView();

            AddButtonToTextBox();

            richTextEditorRibbonBar1.AssociatedRichTextEditor = mailRichTextEditor;
            treeToolWindow.AllowedDockState = AllowedDockState.Floating | AllowedDockState.Docked | AllowedDockState.AutoHide;
            followUpDropDownButton.ActionButton.TextWrap = true;
            ThemeResolutionService.ApplicationThemeName = "TelerikMetroBlue";

            treeToolWindow.TabStrip.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            treeToolWindow.TabStrip.SizeInfo.AbsoluteSize = new Size(250, 0);
            treeToolWindow.TabStrip.SizeInfo.MaximumSize = new Size(250, 0);
            treeToolWindow.TabStrip.SizeInfo.MinimumSize = new Size(250, 0);
        }

        //ribbon
        private void ModifyRibbon()
        {
            foreach (RibbonTab el in richTextEditorRibbonBar1.CommandTabs)
            {
                if (el.Text != "Home")
                {
                    el.Visibility = ElementVisibility.Collapsed;
                }
            }
            richTextEditorRibbonBar1.CommandTabs.Remove(this.homeTab);
            richTextEditorRibbonBar1.CommandTabs.Insert(0, this.homeTab);

            richTextEditorRibbonBar1.CommandTabs[1].Text = "Message";
        }

        //search textbox
        private void AddButtonToTextBox()
        {
            LightVisualElement searchIcon = new LightVisualElement();
            searchIcon.Image = Resources.searchIcon;

            RadTextBoxItem textBoxItem = this.radTextBox1.TextBoxElement.TextBoxItem;
            this.radTextBox1.TextBoxElement.Children.Remove(textBoxItem);

            DockLayoutPanel.SetDock(textBoxItem, Telerik.WinControls.Layouts.Dock.Left);
            DockLayoutPanel.SetDock(searchIcon, Telerik.WinControls.Layouts.Dock.Right);

            DockLayoutPanel dockPanel = new DockLayoutPanel();

            dockPanel.Children.Add(searchIcon);
            dockPanel.Children.Add(textBoxItem);

            this.radTextBox1.TextBoxElement.Children.Add(dockPanel);
        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            mailsGridView.MasterView.TableSearchRow.Search(radTextBox1.Text);
        }

        //Grid
        private void mailsGridView_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if ((EmailStatus)e.RowElement.RowInfo.Cells["Status"].Value == EmailStatus.Unread)
            {
                if (boldFont == null)
                {
                    boldFont = new Font(e.RowElement.Font.Name, e.RowElement.Font.Size, FontStyle.Bold);
                }
                e.RowElement.Font = boldFont;
            }
            else
            {
                e.RowElement.ResetValue(GridRowElement.FontProperty, ValueResetFlags.Local);
            }
        }

        private void mailsGridView_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            mailsGridView.Columns["Content"].IsVisible = false;
            mailsGridView.Columns["Recipient"].IsVisible = false;
            mailsGridView.Columns["Status"].IsVisible = false;

            mailsGridView.Columns["Sender"].HeaderText = "FROM";
            mailsGridView.Columns["Subject"].HeaderText = "SUBJECT";
            mailsGridView.Columns["Received"].HeaderText = "RECEIVED";

            mailsGridView.GroupDescriptors.Clear();
            SortDescriptor sortDesc = new SortDescriptor("Received", ListSortDirection.Descending);
            GroupDescriptor groupDesc = new GroupDescriptor(sortDesc);
            mailsGridView.GroupDescriptors.Add(groupDesc);
        }

        private void mailsGridView_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow is GridViewDataRowInfo)
            {
                Discard();

                e.CurrentRow.Cells["Status"].Value = EmailStatus.Read;

                mailRichTextEditor.Document = (RadDocument)e.CurrentRow.Cells["Content"].Value;
                fromLabel.Text = e.CurrentRow.Cells["Sender"].Value.ToString();
                toLabel.Text = e.CurrentRow.Cells["Recipient"].Value.ToString();
                topicLabel.Text = e.CurrentRow.Cells["Subject"].Value.ToString();
                dateLabel.Text = e.CurrentRow.Cells["Received"].Value.ToString();
            }
        }

        //Tree
        private void BindTreeView()
        {
            mailTreeView.DataSource = EmailService.GetEmailClients();
            mailTreeView.DisplayMember = "Name\\Name";
            mailTreeView.ChildMember = "Emails\\Folders";
            mailTreeView.ExpandAll();
            mailTreeView.SelectedNode = mailTreeView.Nodes[0];
        }

        private void mailTreeView_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (e.Node.DataBoundItem is Folder)
            {
                Folder mailFolder = (Folder)e.Node.DataBoundItem;
                mailsGridView.DataSource = mailFolder.Emails;
            }
            else
            {
                EmailClient mailClient = (EmailClient)e.Node.DataBoundItem;
                mailsGridView.DataSource = mailClient.Emails;
            }
        }

        private void mailTreeView_NodeFormatting(object sender, TreeNodeFormattingEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                Folder mailFolder = (Folder)e.Node.DataBoundItem;
                e.NodeElement.ContentElement.Text = e.Node.Text + " [" + mailFolder.ActiveItems + "]";
            }
        }

        //Methods
        private void PopulateBackstageView()
        {
            richTextEditorRibbonBar1.BackstageControl.Items.Clear();
            richTextEditorRibbonBar1.BackstageControl.BackstageElement.ItemsPanelElement.BackButtonElement.Visibility = ElementVisibility.Visible;
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Info"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Open & Export"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Save As"));
            BackstageTabItem saveAttachments = new BackstageTabItem("Save Attachments");
            saveAttachments.Enabled = false;
            richTextEditorRibbonBar1.BackstageControl.Items.Add(saveAttachments);
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Print"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new SeparatorElement());
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Options"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Exit"));
        }

        private void Reply(string subjectPrefix, bool replyAll)
        {
            readOnlyPanel.Visible = false;
            replyPanel.Visible = true;
            mailRichTextEditor.IsReadOnly = false;

            subjectTextBoxControl.Text = subjectPrefix + mailsGridView.CurrentRow.Cells["Subject"].Value.ToString();
            toTextBoxControl.Text = mailsGridView.CurrentRow.Cells["Recipient"].Value.ToString();

            if (replyAll)
            {
                toTextBoxControl.Text += "; " + mailsGridView.CurrentRow.Cells["Sender"].Value.ToString();
            }

            ChangeMessageTabVisibility(true);
        }

        //Event handlers
        private void allToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                unreadToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                mailsGridView.FilterDescriptors.Clear();
            }
        }

        private void unreadToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (unreadFilter == null)
            {
                unreadFilter = new FilterDescriptor("Status", FilterOperator.IsEqualTo, EmailStatus.Unread);
            }
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                allToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                mailsGridView.FilterDescriptors.Add(unreadFilter);
            }
        }

        private void replyButton_Click(object sender, EventArgs e)
        {
            Reply("Re: ", false);
        }

        private void replyAllButton_Click(object sender, EventArgs e)
        {
            Reply("RE: ", true);
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            Reply("FW: ", false);
        }

        private void popOutButton_Click(object sender, EventArgs e)
        {
            Discard();

            NewEmailForm form = new NewEmailForm((RadDocument)mailRichTextEditor.Document.CreateDeepCopy(), toTextBoxControl.Text, ccTextBoxControl.Text, subjectTextBoxControl.Text);
            form.Show();
        }

        private void ChangeMessageTabVisibility(bool visible)
        {
            richTextEditorRibbonBar1.CommandTabs[1].Visibility = visible ? ElementVisibility.Visible : ElementVisibility.Collapsed;
            ((RibbonTab)richTextEditorRibbonBar1.CommandTabs[1]).IsSelected = visible;
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            Discard();
        }

        private void Discard()
        {
            ChangeMessageTabVisibility(false);

            replyPanel.Visible = false;
            readOnlyPanel.Visible = true;
            mailRichTextEditor.IsReadOnly = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (mailsGridView.CurrentRow is GridViewDataRowInfo)
            {
                mailsGridView.Rows.Remove(mailsGridView.CurrentRow);
            }
        }

        private void readUnreadButton_Click(object sender, EventArgs e)
        {
            if (mailsGridView.CurrentRow is GridViewDataRowInfo)
            {
                if ((EmailStatus)mailsGridView.CurrentRow.Cells["Status"].Value == EmailStatus.Read)
                {
                    mailsGridView.CurrentRow.Cells["Status"].Value = EmailStatus.Unread;
                }
                else
                {
                    mailsGridView.CurrentRow.Cells["Status"].Value = EmailStatus.Read;
                }
                mailsGridView.CurrentRow.InvalidateRow();
            }
        }

        private void newEmailButton_Click(object sender, EventArgs e)
        {
            NewEmailForm form = new NewEmailForm();
            form.Show();
        }

        private void executeCommand_Click(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName(ThemeResolutionService.ApplicationThemeName);

            RadMenuItem clickedItem = sender as RadMenuItem;
            if (clickedItem != null)
            {
                RadMessageBox.Show(clickedItem.Text + " command executed.");
            }

            RadButton clickedButton = sender as RadButton;
            if (clickedButton != null)
            {
                RadMessageBox.Show(clickedButton.Text + " command executed.");
            }
        }
    }
}