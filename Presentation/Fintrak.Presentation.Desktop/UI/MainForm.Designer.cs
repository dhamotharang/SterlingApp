namespace Fintrak.Presentation.Desktop.UI
{
    partial class MainForm
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
            this.mailUserControl1 = new Fintrak.Presentation.Desktop.UI.MailUserControl();
            this.calendarUserControl1 = new Fintrak.Presentation.Desktop.UI.CalendarUserControl();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // mailUserControl1
            // 
            this.mailUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mailUserControl1.Location = new System.Drawing.Point(0, 0);
            this.mailUserControl1.Name = "mailUserControl1";
            this.mailUserControl1.Size = new System.Drawing.Size(1153, 847);
            this.mailUserControl1.TabIndex = 0;
            // 
            // calendarUserControl1
            // 
            this.calendarUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarUserControl1.Location = new System.Drawing.Point(0, 0);
            this.calendarUserControl1.Name = "calendarUserControl1";
            this.calendarUserControl1.Size = new System.Drawing.Size(1153, 847);
            this.calendarUserControl1.TabIndex = 1;
            // 
            // OutlookMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 847);
            this.Controls.Add(this.calendarUserControl1);
            this.Controls.Add(this.mailUserControl1);
            this.Name = "OutlookMailForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "OutlookMailForm";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MailUserControl mailUserControl1;
        private Fintrak.Presentation.Desktop.UI.CalendarUserControl calendarUserControl1;
    }
}