using System;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinForms.Documents.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Fintrak.Presentation.Desktop
{
    public partial class RichTextEditorStatusStrip : UserControl
    {
        public RadRichTextEditor AssociatedRichTextEditor
        {
            get { return _associatedRichTextEditor; }
            set { _associatedRichTextEditor = value; }
        }
        private RadRichTextEditor _associatedRichTextEditor;
        private bool suspendComboUpdate = false;
        private RadDropDownListElement zoomCombo;

        public RichTextEditorStatusStrip()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.AssociatedRichTextEditor != null)
            {
                this.AssociatedRichTextEditor.ScaleFactorChanged += AssociatedRichTextEditor_ScaleFactorChanged;
                this.printLayout.ToggleStateChanged += this.printLayout_ToggleStateChanged;
                this.webLayout.ToggleStateChanged += this.webLayout_ToggleStateChanged;
                this.decreaseZoomButton.Click += this.decreaseZoomButton_Click;
                this.radTrackBarElement1.ValueChanged += this.radTrackBarElement1_ValueChanged;
                this.increaseZoomButton.Click += this.increaseZoomButton_Click;
            }

            zoomCombo = new RadDropDownListElement();
            zoomCombo.DefaultItemsCountInDropDown = 7;
            zoomCombo.MinSize = new System.Drawing.Size(60, 0);
            zoomCombo.SelectedIndexChanged += zoomCombo_SelectedIndexChanged;
            zoomCombo.TextChanged += zoomCombo_TextChanged;
            radStatusStrip1.Items.Insert(3, zoomCombo);

            zoomCombo.Items.Add(new RadListDataItem("25%", 25));
            zoomCombo.Items.Add(new RadListDataItem("50%", 50));
            zoomCombo.Items.Add(new RadListDataItem("75%", 75));
            zoomCombo.Items.Add(new RadListDataItem("100%", 100));
            zoomCombo.Items.Add(new RadListDataItem("150%", 150));
            zoomCombo.Items.Add(new RadListDataItem("200%", 200));
            zoomCombo.Items.Add(new RadListDataItem("500%", 500));
            zoomCombo.SelectedIndex = 3;
        }

        private void AssociatedRichTextEditor_ScaleFactorChanged(object sender, EventArgs e)
        {
            if (!suspendComboUpdate)
            {
                this.radTrackBarElement1.Value = this.AssociatedRichTextEditor.ScaleFactor.Height * 100;
            }
        }

        private void UpdateTrackBarValue(float value)
        {
            suspendComboUpdate = true;
            radTrackBarElement1.Value = value;
            suspendComboUpdate = false;
        }

        private void zoomCombo_TextChanged(object sender, EventArgs e)
        {
            float zoomFactor;
            if (Single.TryParse(zoomCombo.Text, out zoomFactor))
            {
                UpdateTrackBarValue(zoomFactor);
            }
        }

        private void zoomCombo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (e.Position > -1)
            {
                UpdateTrackBarValue(Convert.ToSingle(zoomCombo.Items[e.Position].Value));
            }
        }

        private void radTrackBarElement1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(radTrackBarElement1.Value);

            float scale = Convert.ToSingle(this.radTrackBarElement1.Value / 100.0);
            this.AssociatedRichTextEditor.ScaleFactor = new System.Drawing.SizeF(scale, scale);
            zoomCombo.Text = radTrackBarElement1.Value.ToString() + "%";
        }

        private void decreaseZoomButton_Click(object sender, EventArgs e)
        {
            radTrackBarElement1.Value -= 10;
        }

        private void increaseZoomButton_Click(object sender, EventArgs e)
        {
            radTrackBarElement1.Value += 10;
        }

        private void printLayout_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                this.AssociatedRichTextEditor.LayoutMode = DocumentLayoutMode.Paged;
                webLayout.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;

                webLayout.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
                printLayout.ButtonFillElement.Visibility = ElementVisibility.Visible;
            }
        }

        private void webLayout_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                this.AssociatedRichTextEditor.LayoutMode = DocumentLayoutMode.Flow;
                printLayout.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;

                webLayout.ButtonFillElement.Visibility = ElementVisibility.Visible;
                printLayout.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
            }
        }
    }
}
