using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;

namespace Fintrak.Presentation.Desktop.UI
{
    public partial class CalendarUserControl : UserControl
    {
        #region Fields

        Resource teamResource;
        Resource personalResource;
        Resource companyResource;

        #endregion

        #region Constructor

        public CalendarUserControl()
        {
            InitializeComponent();

            WireEvents();

            SetupScheduler();

            calendarPageView.ViewElement.Header.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

            toolWindow1.ToolCaptionButtons = Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.AutoHide | Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.SystemMenu;

            toolWindow1.TabStrip.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            toolWindow1.TabStrip.SizeInfo.AbsoluteSize = new Size(250, 0);
            toolWindow1.TabStrip.SizeInfo.MaximumSize = new Size(250, 0);
            toolWindow1.TabStrip.SizeInfo.MinimumSize = new Size(250, 0);
        }

        #endregion

        #region Methods

        private void WireEvents()
        {
            this.companyCheckBox.ToggleStateChanged += this.companyCheckBox_ToggleStateChanged;
            this.teamCheckBox.ToggleStateChanged += this.teamCheckBox_ToggleStateChanged;
            this.personalCheckBox.ToggleStateChanged += this.personalCheckBox_ToggleStateChanged;
            this.radCalendar1.SelectionChanged += this.radCalendar1_SelectionChanged;
            this.radScheduler1.ActiveViewChanged += this.radScheduler1_ActiveViewChanged;
            this.newAppointmentButton.Click += this.newAppointmentButton_Click;
            this.deleteAppointmentButton.Click += this.deleteAppointmentButton_Click;
            this.todayButton.Click += this.todayButton_Click;
            this.nextRangeButton.Click += this.nextRangeButton_Click;
            this.dayViewButton.Click += this.dayViewButton_Click;
            this.weekViewButton.Click += this.weekViewButton_Click;
            this.workWeekViewButton.Click += this.workWeekViewButton_Click;
            this.monthViewButton.Click += this.monthViewButton_Click;
            this.timelineViewButton.Click += this.timelineViewButton_Click;
            this.redCategoryMenuItem.Click += this.redCategoryMenuItem_Click;
            this.greenCategoryMenuItem.Click += this.greenCategoryMenuItem_Click;
            this.blueCategoryMenuItem.Click += this.blueCategoryMenuItem_Click;
            this.freeMarkerMenuItem.Click += this.freeMarkerMenuItem_Click;
            this.busyMarkerMenuItem.Click += this.busyMarkerMenuItem_Click;
            this.tentativeMarkerMenuItem.Click += this.tentativeMarkerMenuItem_Click;
            this.oooMarkerMenuItem.Click += this.oooMarkerMenuItem_Click;
            this.radTrackBarElement1.ValueChanged += this.radTrackBarElement1_ValueChanged;
        }

        private void SetupScheduler()
        {
            radScheduler1.Appointments.CollectionChanged += Appointments_CollectionChanged;

            radScheduler1.ActiveViewType = SchedulerViewType.WorkWeek;

            //statuses
            AppointmentStatusInfo.DefaultStatusId = -1;
            foreach (AppointmentStatusInfo status in radScheduler1.Statuses)
            {
                switch (status.DisplayName)
                {
                    case "Free":
                        status.BackColor = Color.FromArgb(48, 155, 70);
                        break;
                    case "Busy":
                        status.BackColor = Color.FromArgb(230, 30, 38);
                        break;
                    case "Tentative":
                        status.BackColor = Color.FromArgb(65, 34, 155);
                        status.FillType = AppointmentStatusFillType.Solid;
                        break;
                    case "Unavailable":
                        status.BackColor = Color.FromArgb(241, 199, 0);
                        break;
                }
            }

            //backgrounds
            this.radScheduler1.Backgrounds.Add(new AppointmentBackgroundInfo(12, "Red", Color.FromArgb(244, 208, 199), Color.FromArgb(244, 208, 199), Color.Black, Color.FromArgb(244, 208, 199)));
            this.radScheduler1.Backgrounds.Add(new AppointmentBackgroundInfo(13, "Green", Color.FromArgb(189, 230, 215), Color.FromArgb(189, 230, 215), Color.Black, Color.FromArgb(189, 230, 215)));
            this.radScheduler1.Backgrounds.Add(new AppointmentBackgroundInfo(14, "Blue", Color.FromArgb(200, 216, 233), Color.FromArgb(200, 216, 233), Color.Black, Color.FromArgb(200, 216, 233)));

            //resources
            teamResource = new Resource(new EventId("Team"), "Team");
            teamResource.Color = Color.FromArgb(165, 211, 211);
            personalResource = new Resource(new EventId("Personal"), "Personal");
            personalResource.Color = Color.FromArgb(255, 239, 191);
            companyResource = new Resource(new EventId("Company"), "Company");
            companyResource.Color = Color.FromArgb(148, 176, 243);

            //appointments
            DateTime today = DateTime.Now;

            Appointment appointment = new Appointment();
            appointment.Start = new DateTime(today.Year, today.Month, today.Day, 8, 0, 0, DateTimeKind.Local);
            appointment.End = new DateTime(today.Year, today.Month, today.Day, 9, 0, 0, DateTimeKind.Local);
            appointment.Summary = "Morning Meeting";
            appointment.ResourceId = teamResource.Id;
            appointment.RecurrenceRule = new DailyRecurrenceRule(appointment.Start, 1, 5);
            this.radScheduler1.Appointments.Add(appointment);

            appointment = new Appointment();
            appointment.Start = new DateTime(today.Year, today.Month, today.Day, 14, 0, 0, DateTimeKind.Local);
            appointment.End = new DateTime(today.Year, today.Month, today.Day, 14, 30, 0, DateTimeKind.Local);
            appointment.Summary = "Coffee Break";
            appointment.ResourceId = personalResource.Id;
            appointment.RecurrenceRule = new DailyRecurrenceRule(appointment.Start, 1, 5);
            this.radScheduler1.Appointments.Add(appointment);

            appointment = new Appointment();
            appointment.Start = new DateTime(today.Year, today.Month, today.Day, 16, 0, 0, DateTimeKind.Local);
            appointment.End = new DateTime(today.Year, today.Month, today.Day, 17, 00, 0, DateTimeKind.Local);
            appointment.Summary = "Client Meeting";
            appointment.ResourceId = companyResource.Id;
            appointment.RecurrenceRule = new DailyRecurrenceRule(appointment.Start, 1, 5);
            this.radScheduler1.Appointments.Add(appointment);

            DateTime friday = today.AddDays(-(int)today.DayOfWeek).AddDays(5);

            appointment = new Appointment();
            appointment.Start = new DateTime(today.Year, today.Month, friday.Day, 18, 0, 0, DateTimeKind.Local);
            appointment.End = new DateTime(today.Year, today.Month, friday.Day, 23, 00, 0, DateTimeKind.Local);
            appointment.Summary = "Release Party";
            appointment.ResourceId = companyResource.Id;
            this.radScheduler1.Appointments.Add(appointment);
        }

        private void ToggleGroupingState()
        {
            if (personalCheckBox.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off &&
                teamCheckBox.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off &&
                companyCheckBox.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
            {
                this.radScheduler1.GroupType = Telerik.WinControls.UI.GroupType.None;
            }
        }

        private void InitializeCalendar()
        {
            MultiMonthViewElement viewElement = this.radCalendar1.CalendarElement.CalendarVisualElement as MultiMonthViewElement;

            this.radCalendar1.CalendarElement.Margin = new Padding(0, 0, 0, 14);

            if (viewElement != null)
            {
                CalendarMultiMonthViewTableElement table = viewElement.GetMultiTableElement();

                foreach (MonthViewElement monthView in table.Children)
                {
                    monthView.TitleElement.Margin = new Padding(-4, -2, -2, -2);
                    monthView.TitleElement.Padding = new Padding(3);

                    foreach (CalendarCellElement cell in monthView.TableElement.Children)
                    {
                        bool headerCell = (bool)cell.GetValue(CalendarCellElement.IsHeaderCellProperty);
                        if (headerCell)
                            continue;

                        SchedulerDayView view = new SchedulerDayView();
                        view.DayCount = 1;
                        view.StartDate = cell.Date;
                        view.GetViewContainingDate(cell.Date);

                        view.UpdateAppointments(this.radScheduler1.Appointments);

                        if (view.Appointments.Count > 0)
                        {
                            cell.Font = new Font(cell.Font, FontStyle.Bold);
                        }
                        else
                        {
                            cell.Font = this.radCalendar1.Font;
                        }
                    }
                }
            }
        }

        private NavigationStepTypes StepsByScalling(Timescales timescale, ref int navigationStep)
        {
            NavigationStepTypes navigationStepType = NavigationStepTypes.Day;
            switch (timescale)
            {
                case Timescales.Minutes:
                    navigationStepType = NavigationStepTypes.Minute;
                    navigationStep = 15;
                    break;
                case Timescales.HalfHour:
                    navigationStepType = NavigationStepTypes.Minute;
                    navigationStep = 30;
                    break;
                case Timescales.Hours:
                    navigationStepType = NavigationStepTypes.Hour;
                    break;
                case Timescales.Days:
                    navigationStepType = NavigationStepTypes.Day;
                    break;
                case Timescales.Weeks:
                    navigationStepType = NavigationStepTypes.Week;
                    break;
                case Timescales.Months:
                    navigationStepType = NavigationStepTypes.Month;
                    break;
                case Timescales.Years:
                    navigationStepType = NavigationStepTypes.Year;
                    break;
            }

            return navigationStepType;
        }

        private void SetBackground(int backgroundId)
        {
            if (radScheduler1.SelectionBehavior.SelectedAppointment != null)
            {
                radScheduler1.SelectionBehavior.SelectedAppointment.BackgroundId = backgroundId;

                if (radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent != null &&
                    !radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent.Exceptions.Contains(radScheduler1.SelectionBehavior.SelectedAppointment))
                {
                    radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent.Exceptions.Add(radScheduler1.SelectionBehavior.SelectedAppointment);
                }
            }
        }

        private void SetStatus(int statusId)
        {
            if (radScheduler1.SelectionBehavior.SelectedAppointment != null)
            {
                radScheduler1.SelectionBehavior.SelectedAppointment.StatusId = statusId;

                if (radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent != null &&
                    !radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent.Exceptions.Contains(radScheduler1.SelectionBehavior.SelectedAppointment))
                {
                    radScheduler1.SelectionBehavior.SelectedAppointment.MasterEvent.Exceptions.Add(radScheduler1.SelectionBehavior.SelectedAppointment);
                }
            }
        }

        private void ToggleResource(ToggleState toggleState, Resource resource)
        {
            if (Enum.Equals(toggleState, Telerik.WinControls.Enumerations.ToggleState.On))
            {
                this.radScheduler1.Resources.Add(resource);
                this.radScheduler1.GroupType = Telerik.WinControls.UI.GroupType.Resource;
            }
            else
            {
                this.radScheduler1.Resources.Remove(resource);
                ToggleGroupingState();
            }

            radScheduler1.ActiveView.ResourcesPerView = radScheduler1.Resources.Count;
        }


        #endregion

        #region Event handlers

        private void Appointments_CollectionChanged(object sender, Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
        {
            InitializeCalendar();
        }

        private void radScheduler1_ActiveViewChanged(object sender, SchedulerViewChangedEventArgs e)
        {
            toolWindow2.Text = e.NewView.ViewType.ToString();

            if (e.NewView is SchedulerDayViewBase)
            {
                SchedulerDayViewBase dayViewBase = (SchedulerDayViewBase)e.NewView;
                dayViewBase.DayCount = 1;
                dayViewBase.RulerStartScale = 7;
                dayViewBase.RulerEndScale = 18;
                dayViewBase.RangeFactor = ScaleRange.QuarterHour;
            }
        }

        private void newAppointmentButton_Click(object sender, EventArgs e)
        {
            radScheduler1.AddNewAppointmentWithDialog(new DateTimeInterval(DateTime.Now, TimeSpan.FromHours(1)), false, null);
        }

        private void deleteAppointmentButton_Click(object sender, EventArgs e)
        {
            radScheduler1.SchedulerInputBehavior.DeleteSelectedAppointments();
        }

        private void todayButton_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now;

            if (this.radScheduler1.ActiveViewType == SchedulerViewType.Month)
            {
                startDate = new DateTime(startDate.Year, startDate.Month, 1);
                this.radScheduler1.GetMonthView().WeekCount = DateHelper.GetMonthDisplayWeeks(startDate, this.radScheduler1.Culture.DateTimeFormat);
            }

            radScheduler1.ActiveView.StartDate = startDate;
        }

        private void nextRangeButton_Click(object sender, EventArgs e)
        {
            int navigationStep = 0;
            NavigationStepTypes navigationStepType = NavigationStepTypes.Day;
            switch (this.radScheduler1.ActiveViewType)
            {
                case SchedulerViewType.Day:
                case SchedulerViewType.MultiDay:
                    navigationStep = 1;
                    navigationStepType = NavigationStepTypes.Day;
                    break;
                case SchedulerViewType.Timeline:
                    navigationStep = 1;
                    navigationStepType = StepsByScalling(this.radScheduler1.GetTimelineView().GetScaling().Timescale, ref navigationStep);
                    break;
                case SchedulerViewType.Week:
                case SchedulerViewType.WorkWeek:
                    navigationStep = 1;
                    navigationStepType = NavigationStepTypes.Week;
                    break;
                case SchedulerViewType.Month:
                    navigationStep = 1;
                    if (!this.radScheduler1.GetMonthView().ShowWeekend)
                    {
                        navigationStep += 2;
                    }
                    DateTime endDate = this.radScheduler1.ActiveView.EndDate.AddDays(navigationStep);
                    DateTime dtStart = this.radScheduler1.ActiveView.StartDate;
                    DateTime dtStartMonth = new DateTime(endDate.Year, endDate.Month, 1);
                    navigationStep = dtStartMonth.Subtract(dtStart).Days;
                    navigationStepType = NavigationStepTypes.Day;
                    this.radScheduler1.GetMonthView().WeekCount = DateHelper.GetMonthDisplayWeeks(dtStartMonth, this.radScheduler1.Culture.DateTimeFormat);
                    break;
            }

            RadScheduler.NavigateToNextViewCommand.ExecuteCommand(this.radScheduler1, navigationStepType, navigationStep);
        }

        private void dayViewButton_Click(object sender, EventArgs e)
        {
            radScheduler1.ActiveViewType = SchedulerViewType.Day;
        }

        private void weekViewButton_Click(object sender, EventArgs e)
        {
            radScheduler1.ActiveViewType = SchedulerViewType.Week;
        }

        private void workWeekViewButton_Click(object sender, EventArgs e)
        {
            radScheduler1.ActiveViewType = SchedulerViewType.WorkWeek;
        }

        private void monthViewButton_Click(object sender, EventArgs e)
        {
            radScheduler1.ActiveViewType = SchedulerViewType.Month;
        }

        private void timelineViewButton_Click(object sender, EventArgs e)
        {
            radScheduler1.ActiveViewType = SchedulerViewType.Timeline;
        }

        private void redCategoryMenuItem_Click(object sender, EventArgs e)
        {
            SetBackground(12);
        }

        private void greenCategoryMenuItem_Click(object sender, EventArgs e)
        {
            SetBackground(13);
        }

        private void blueCategoryMenuItem_Click(object sender, EventArgs e)
        {
            SetBackground(14);
        }

        private void freeMarkerMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(1);
        }

        private void oooMarkerMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(3);
        }

        private void busyMarkerMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(2);
        }

        private void tentativeMarkerMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(4);
        }

        private void personalCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleResource(args.ToggleState, personalResource);
        }

        private void teamCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleResource(args.ToggleState, teamResource);
        }

        private void companyCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleResource(args.ToggleState, companyResource);
        }

        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.radCalendar1.SelectedDates.Count > 0)
            {
                this.radScheduler1.ActiveView.StartDate = this.radCalendar1.SelectedDate;
            }
        }

        private void radTrackBarElement1_ValueChanged(object sender, EventArgs e)
        {
            switch (radScheduler1.ActiveViewType)
            {
                case SchedulerViewType.Day:
                    radScheduler1.GetDayView().RulerScaleSize = (int)(radTrackBarElement1.Value * 10);
                    break;
                case SchedulerViewType.Week:
                    radScheduler1.GetWeekView().RulerScaleSize = (int)(radTrackBarElement1.Value * 10);
                    break;
                case SchedulerViewType.WorkWeek:
                    radScheduler1.GetWeekView().RulerScaleSize = (int)(radTrackBarElement1.Value * 10);
                    break;
                case SchedulerViewType.Month:
                    radScheduler1.GetMonthView().WeekCount = (int)(radTrackBarElement1.Value * 3);
                    break;
                case SchedulerViewType.Timeline:
                    radScheduler1.GetTimelineView().GetScaling().DisplayedCellsCount = (int)(radTrackBarElement1.Value * 3);
                    break;
            }
        }

        #endregion
    }
}