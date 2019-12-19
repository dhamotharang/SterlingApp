using System;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;

namespace Fintrak.Presentation.Desktop
{
    public partial class NewEmailForm : Telerik.WinControls.UI.RadForm
    {
        public NewEmailForm()
        {
            InitializeComponent();

            Initialize();
        }

        public NewEmailForm(RadDocument rteContnt, string to, string cc, string subject)
        {
            InitializeComponent();

            Initialize();

            mailRichTextEditor.Document = rteContnt;
            toTextBoxControl.Text = to;
            ccTextBoxControl.Text = cc;
            subjectTextBoxControl.Text = subject;
        }

        private void Initialize()
        {
            //enable themeing
            ((RadRibbonFormBehavior)this.FormBehavior).AllowTheming = false;

            //preselect tab
            ((RibbonTab)richTextEditorRibbonBar1.CommandTabs[0]).IsSelected = true;

            //adjust backstageview
            richTextEditorRibbonBar1.BackstageControl.Items.Clear();
            richTextEditorRibbonBar1.BackstageControl.BackstageElement.ItemsPanelElement.BackButtonElement.Visibility = ElementVisibility.Visible;
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Info"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Save As"));
            BackstageTabItem saveAttachments = new BackstageTabItem("Save Attachments");
            saveAttachments.Enabled = false;
            richTextEditorRibbonBar1.BackstageControl.Items.Add(saveAttachments);
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Print"));
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem("Close"));
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            RadButton clickedButton = sender as RadButton;
            if (clickedButton != null)
            {
                RadMessageBox.SetThemeName(this.ThemeName);
                RadMessageBox.Show(clickedButton.Text + " command executed.");
            }
        }
    }
}
