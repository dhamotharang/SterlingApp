namespace Fintrak.Presentation.Desktop
{
    partial class NewEmailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEmailForm));
            this.replyPanel = new Telerik.WinControls.UI.RadPanel();
            this.subjectTextBoxControl = new Telerik.WinControls.UI.RadTextBoxControl();
            this.ccTextBoxControl = new Telerik.WinControls.UI.RadTextBoxControl();
            this.toTextBoxControl = new Telerik.WinControls.UI.RadTextBoxControl();
            this.subjectLabel = new Telerik.WinControls.UI.RadLabel();
            this.ccLabel = new Telerik.WinControls.UI.RadLabel();
            this.toLabelReply = new Telerik.WinControls.UI.RadLabel();
            this.radButton6 = new Telerik.WinControls.UI.RadButton();
            this.richTextEditorRibbonBar1 = new Telerik.WinControls.UI.RichTextEditorRibbonBar();
            this.mailRichTextEditor = new Telerik.WinControls.UI.RadRichTextEditor();
            this.richTextEditorStatusStrip1 = new Fintrak.Presentation.Desktop.RichTextEditorStatusStrip();
            this.radRibbonFormBehavior1 = new Telerik.WinControls.UI.RadRibbonFormBehavior();
            this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            ((System.ComponentModel.ISupportInitialize)(this.replyPanel)).BeginInit();
            this.replyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subjectTextBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccTextBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toTextBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subjectLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toLabelReply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richTextEditorRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailRichTextEditor)).BeginInit();
            this.mailRichTextEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // replyPanel
            // 
            this.replyPanel.Controls.Add(this.subjectTextBoxControl);
            this.replyPanel.Controls.Add(this.ccTextBoxControl);
            this.replyPanel.Controls.Add(this.toTextBoxControl);
            this.replyPanel.Controls.Add(this.subjectLabel);
            this.replyPanel.Controls.Add(this.ccLabel);
            this.replyPanel.Controls.Add(this.toLabelReply);
            this.replyPanel.Controls.Add(this.radButton6);
            this.replyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.replyPanel.Location = new System.Drawing.Point(0, 161);
            this.replyPanel.Name = "replyPanel";
            this.replyPanel.Size = new System.Drawing.Size(1287, 95);
            this.replyPanel.TabIndex = 1;
            this.replyPanel.ThemeName = "TelerikMetroBlue";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.replyPanel.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // subjectTextBoxControl
            // 
            this.subjectTextBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subjectTextBoxControl.Location = new System.Drawing.Point(119, 66);
            this.subjectTextBoxControl.Name = "subjectTextBoxControl";
            this.subjectTextBoxControl.Size = new System.Drawing.Size(1165, 20);
            this.subjectTextBoxControl.TabIndex = 8;
            this.subjectTextBoxControl.ThemeName = "TelerikMetroBlue";
            // 
            // ccTextBoxControl
            // 
            this.ccTextBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ccTextBoxControl.Location = new System.Drawing.Point(119, 36);
            this.ccTextBoxControl.Name = "ccTextBoxControl";
            this.ccTextBoxControl.Size = new System.Drawing.Size(1165, 20);
            this.ccTextBoxControl.TabIndex = 7;
            this.ccTextBoxControl.ThemeName = "TelerikMetroBlue";
            // 
            // toTextBoxControl
            // 
            this.toTextBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toTextBoxControl.Location = new System.Drawing.Point(119, 6);
            this.toTextBoxControl.Name = "toTextBoxControl";
            this.toTextBoxControl.Size = new System.Drawing.Size(1165, 20);
            this.toTextBoxControl.TabIndex = 6;
            this.toTextBoxControl.ThemeName = "TelerikMetroBlue";
            // 
            // subjectLabel
            // 
            this.subjectLabel.Location = new System.Drawing.Point(66, 66);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(47, 19);
            this.subjectLabel.TabIndex = 5;
            this.subjectLabel.Text = "Subject";
            this.subjectLabel.ThemeName = "TelerikMetroBlue";
            // 
            // ccLabel
            // 
            this.ccLabel.Location = new System.Drawing.Point(66, 37);
            this.ccLabel.Name = "ccLabel";
            this.ccLabel.Size = new System.Drawing.Size(28, 19);
            this.ccLabel.TabIndex = 5;
            this.ccLabel.Text = "Cc...";
            this.ccLabel.ThemeName = "TelerikMetroBlue";
            // 
            // toLabelReply
            // 
            this.toLabelReply.Location = new System.Drawing.Point(66, 8);
            this.toLabelReply.Name = "toLabelReply";
            this.toLabelReply.Size = new System.Drawing.Size(28, 19);
            this.toLabelReply.TabIndex = 4;
            this.toLabelReply.Text = "To...";
            this.toLabelReply.ThemeName = "TelerikMetroBlue";
            // 
            // radButton6
            // 
            this.radButton6.Image = global::Fintrak.Presentation.Desktop.Properties.Resources.NewEmail;
            this.radButton6.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton6.Location = new System.Drawing.Point(12, 6);
            this.radButton6.Name = "radButton6";
            this.radButton6.Size = new System.Drawing.Size(40, 62);
            this.radButton6.TabIndex = 1;
            this.radButton6.Text = "Send";
            this.radButton6.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.radButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.radButton6.ThemeName = "TelerikMetroBlue";
            this.radButton6.Click += new System.EventHandler(this.radButton6_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton6.GetChildAt(0))).Image = global::Fintrak.Presentation.Desktop.Properties.Resources.NewEmail;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton6.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton6.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton6.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton6.GetChildAt(0))).Text = "Send";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton6.GetChildAt(0).GetChildAt(2))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton6.GetChildAt(0).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // richTextEditorRibbonBar1
            // 
            this.richTextEditorRibbonBar1.ApplicationMenuStyle = Telerik.WinControls.UI.ApplicationMenuStyle.BackstageView;
            this.richTextEditorRibbonBar1.AssociatedRichTextEditor = this.mailRichTextEditor;
            this.richTextEditorRibbonBar1.BuiltInStylesVersion = Telerik.WinForms.Documents.Model.Styles.BuiltInStylesVersion.Office2013;
            this.richTextEditorRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.richTextEditorRibbonBar1.Name = "richTextEditorRibbonBar1";
            this.richTextEditorRibbonBar1.Size = new System.Drawing.Size(1287, 161);
            this.richTextEditorRibbonBar1.StartButtonImage = ((System.Drawing.Image)(resources.GetObject("richTextEditorRibbonBar1.StartButtonImage")));
            this.richTextEditorRibbonBar1.TabIndex = 2;
            this.richTextEditorRibbonBar1.TabStop = false;
            this.richTextEditorRibbonBar1.ThemeName = "TelerikMetroBlue";
            ((Telerik.WinControls.UI.RadRibbonBarElement)(this.richTextEditorRibbonBar1.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "Message";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(2))).Text = "Page Layout";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(3))).Text = "References";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(3))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(4))).Text = "Mailings";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(4))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(5))).Text = "Review";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(5))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(6))).Text = "View";
            ((Telerik.WinControls.UI.RichTextEditorRibbonUI.RichTextEditorRibbonTab)(this.richTextEditorRibbonBar1.GetChildAt(0).GetChildAt(4).GetChildAt(0).GetChildAt(0).GetChildAt(6))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // mailRichTextEditor
            // 
            this.mailRichTextEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.mailRichTextEditor.Controls.Add(this.richTextEditorStatusStrip1);
            this.mailRichTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mailRichTextEditor.Location = new System.Drawing.Point(0, 256);
            this.mailRichTextEditor.Name = "mailRichTextEditor";
            this.mailRichTextEditor.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(182)))), ((int)(((byte)(224)))), ((int)(((byte)(243)))));
            this.mailRichTextEditor.SelectionStroke = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(205)))), ((int)(((byte)(217)))));
            this.mailRichTextEditor.Size = new System.Drawing.Size(1287, 696);
            this.mailRichTextEditor.TabIndex = 3;
            this.mailRichTextEditor.ThemeName = "TelerikMetroBlue";
            // 
            // richTextEditorStatusStrip1
            // 
            this.richTextEditorStatusStrip1.AssociatedRichTextEditor = this.mailRichTextEditor;
            this.richTextEditorStatusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.richTextEditorStatusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextEditorStatusStrip1.Location = new System.Drawing.Point(0, 671);
            this.richTextEditorStatusStrip1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextEditorStatusStrip1.Name = "richTextEditorStatusStrip1";
            this.richTextEditorStatusStrip1.Size = new System.Drawing.Size(1287, 25);
            this.richTextEditorStatusStrip1.TabIndex = 0;
            // 
            // radRibbonFormBehavior1
            // 
            this.radRibbonFormBehavior1.Form = this;
            // 
            // NewEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 952);
            this.Controls.Add(this.mailRichTextEditor);
            this.Controls.Add(this.replyPanel);
            this.Controls.Add(this.richTextEditorRibbonBar1);
            this.FormBehavior = this.radRibbonFormBehavior1;
            this.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None;
            this.Name = "NewEmailForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "";
            this.ThemeName = "TelerikMetroBlue";
            ((System.ComponentModel.ISupportInitialize)(this.replyPanel)).EndInit();
            this.replyPanel.ResumeLayout(false);
            this.replyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subjectTextBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccTextBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toTextBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subjectLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toLabelReply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richTextEditorRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailRichTextEditor)).EndInit();
            this.mailRichTextEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel replyPanel;
        private Telerik.WinControls.UI.RadTextBoxControl subjectTextBoxControl;
        private Telerik.WinControls.UI.RadTextBoxControl ccTextBoxControl;
        private Telerik.WinControls.UI.RadTextBoxControl toTextBoxControl;
        private Telerik.WinControls.UI.RadLabel subjectLabel;
        private Telerik.WinControls.UI.RadLabel ccLabel;
        private Telerik.WinControls.UI.RadLabel toLabelReply;
        private Telerik.WinControls.UI.RadButton radButton6;
        private Telerik.WinControls.UI.RichTextEditorRibbonBar richTextEditorRibbonBar1;
        private Telerik.WinControls.UI.RadRichTextEditor mailRichTextEditor;
        private Telerik.WinControls.UI.RadRibbonFormBehavior radRibbonFormBehavior1;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme telerikMetroBlueTheme1;
        private RichTextEditorStatusStrip richTextEditorStatusStrip1;
    }
}
