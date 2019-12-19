namespace Fintrak.Presentation.Desktop
{
    partial class RichTextEditorStatusStrip
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextEditorStatusStrip));
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radLabelElement1 = new Telerik.WinControls.UI.RadLabelElement();
            this.printLayout = new Telerik.WinControls.UI.RadToggleButtonElement();
            this.webLayout = new Telerik.WinControls.UI.RadToggleButtonElement();
            this.decreaseZoomButton = new Telerik.WinControls.UI.RadButtonElement();
            this.radTrackBarElement1 = new Telerik.WinControls.UI.RadTrackBarElement();
            this.increaseZoomButton = new Telerik.WinControls.UI.RadButtonElement();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            this.SuspendLayout();
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radLabelElement1,
            this.printLayout,
            this.webLayout,
            this.decreaseZoomButton,
            this.radTrackBarElement1,
            this.increaseZoomButton});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 0);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(1212, 24);
            this.radStatusStrip1.TabIndex = 3;
            this.radStatusStrip1.Text = "radStatusStrip1";
            this.radStatusStrip1.ThemeName = "TelerikMetroBlue";
            ((Telerik.WinControls.UI.RadStatusBarElement)(this.radStatusStrip1.GetChildAt(0))).Text = "radStatusStrip1";
            ((Telerik.WinControls.UI.RadStatusBarElement)(this.radStatusStrip1.GetChildAt(0))).Margin = new System.Windows.Forms.Padding(0, -2, 0, 0);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radStatusStrip1.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
            // 
            // radLabelElement1
            // 
            this.radLabelElement1.AccessibleDescription = "radLabelElement1";
            this.radLabelElement1.AccessibleName = "radLabelElement1";
            this.radLabelElement1.Name = "radLabelElement1";
            this.radStatusStrip1.SetSpring(this.radLabelElement1, true);
            this.radLabelElement1.Text = "";
            this.radLabelElement1.TextWrap = true;
            // 
            // printLayout
            // 
            this.printLayout.AccessibleDescription = "radButtonElement1";
            this.printLayout.AccessibleName = "radButtonElement1";
            this.printLayout.BackColor = System.Drawing.Color.Transparent;
            this.printLayout.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.printLayout.Image = global::Fintrak.Presentation.Desktop.Properties.Resources.printLayout;
            this.printLayout.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.printLayout.Name = "printLayout";
            this.printLayout.ReadOnly = false;
            this.printLayout.ShowBorder = false;
            this.radStatusStrip1.SetSpring(this.printLayout, false);
            this.printLayout.Text = "";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.printLayout.GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // webLayout
            // 
            this.webLayout.AccessibleDescription = "radButtonElement2";
            this.webLayout.AccessibleName = "radButtonElement2";
            this.webLayout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.webLayout.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.webLayout.Image = global::Fintrak.Presentation.Desktop.Properties.Resources.webLayout;
            this.webLayout.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.webLayout.Name = "webLayout";
            this.webLayout.ReadOnly = false;
            this.webLayout.ShowBorder = false;
            this.radStatusStrip1.SetSpring(this.webLayout, false);
            this.webLayout.Text = "";
            this.webLayout.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // decreaseZoomButton
            // 
            this.decreaseZoomButton.Image = ((System.Drawing.Image)(resources.GetObject("decreaseZoomButton.Image")));
            this.decreaseZoomButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.decreaseZoomButton.MinSize = new System.Drawing.Size(20, 18);
            this.decreaseZoomButton.Name = "decreaseZoomButton";
            this.decreaseZoomButton.ShowBorder = false;
            this.radStatusStrip1.SetSpring(this.decreaseZoomButton, false);
            this.decreaseZoomButton.Text = "";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.decreaseZoomButton.GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // radTrackBarElement1
            // 
            this.radTrackBarElement1.AccessibleDescription = "radTrackBarElement1";
            this.radTrackBarElement1.AccessibleName = "radTrackBarElement1";
            this.radTrackBarElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radTrackBarElement1.DrawFill = false;
            this.radTrackBarElement1.FitInAvailableSize = true;
            this.radTrackBarElement1.LargeChange = 10;
            this.radTrackBarElement1.LargeTickFrequency = 10;
            this.radTrackBarElement1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.radTrackBarElement1.Maximum = 800F;
            this.radTrackBarElement1.Minimum = 25F;
            this.radTrackBarElement1.Name = "radTrackBarElement1";
            this.radTrackBarElement1.SmallTickFrequency = 10;
            this.radStatusStrip1.SetSpring(this.radTrackBarElement1, false);
            this.radTrackBarElement1.Text = "";
            this.radTrackBarElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radTrackBarElement1.Value = 25F;
            // 
            // increaseZoomButton
            // 
            this.increaseZoomButton.Image = global::Fintrak.Presentation.Desktop.Properties.Resources.plus;
            this.increaseZoomButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.increaseZoomButton.MinSize = new System.Drawing.Size(20, 18);
            this.increaseZoomButton.Name = "increaseZoomButton";
            this.increaseZoomButton.ShowBorder = false;
            this.radStatusStrip1.SetSpring(this.increaseZoomButton, false);
            this.increaseZoomButton.Text = "";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.increaseZoomButton.GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // RichTextEditorStatusStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.radStatusStrip1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RichTextEditorStatusStrip";
            this.Size = new System.Drawing.Size(1212, 24);
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadLabelElement radLabelElement1;
        private Telerik.WinControls.UI.RadToggleButtonElement printLayout;
        private Telerik.WinControls.UI.RadToggleButtonElement webLayout;
        private Telerik.WinControls.UI.RadButtonElement decreaseZoomButton;
        private Telerik.WinControls.UI.RadTrackBarElement radTrackBarElement1;
        private Telerik.WinControls.UI.RadButtonElement increaseZoomButton;
    }
}
