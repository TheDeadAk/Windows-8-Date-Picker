using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ControlsLiberyWin8
{
    public sealed partial class DatePicker : UserControl
    {
        public DatePicker()
        {
            this.InitializeComponent();
            init();
        }

        private void init()
        {
            Source _source = new Source();
            if (_source != null)
            {
                if (_source.Years.Count == 0 && _source.Months.Count == 0 && _source.Days.Count == 0)
                {
                    _source.GetYears();
                    _source.GetMonths();
                    _source.GetDays(DateTime.Now.Month, DateTime.Now.Year);
                }
                this.setSource(_source.Days, _source.Months, _source.Years);
            }
        }

        private void changeday(object sender, SelectionChangedEventArgs e)
        {
            int d = dayCombo.SelectedIndex;
            Source day = new Source();
            day.GetDays(monthCombo.SelectedIndex + 1, yearCombo.SelectedIndex + 2012);
            dayCombo.ItemsSource = day.Days;
            if (dayCombo.Items.Count > d)
            {
                dayCombo.SelectedIndex = d;
            }
            else
            {
                dayCombo.SelectedIndex = dayCombo.Items.Count - 1;
            }
        }

        private void setSource(ObservableCollection<Days> day, ObservableCollection<Months> month, ObservableCollection<Years> year)
        {

            dayCombo.ItemsSource = day;
            monthCombo.ItemsSource = month;
            yearCombo.ItemsSource = year;
            dayCombo.SelectedIndex = DateTime.Now.Day - 1;
            monthCombo.SelectedIndex = DateTime.Now.Month - 1;
            yearCombo.SelectedIndex = DateTime.Now.Year - 2012;
        }

        /// <summary>
        /// Get the selected Date
        /// </summary>
        /// <returns>Return string format dd.mm.yy</returns>
        public string getSelectedDate()
        {
            var day = (((Days)dayCombo.SelectedItem)._day).ToString();
            var month = (((Months)monthCombo.SelectedItem)._month).ToString();
            var year = (((Years)yearCombo.SelectedItem)._decade).ToString();
            if (Convert.ToInt16(day) < 10)
            {
                day = "0" + day;
            }
            if (Convert.ToInt16(month) < 10)
            {
                month = "0" + month;
            }

            return day + "." + month + "." + year;
        }

        /// <summary>
        /// Get the selected Date
        /// </summary>
        /// <returns>Return string format dd.mm.yyyy</returns>
        public string getSelectedDateLong()
        {
            var day = (((Days)dayCombo.SelectedItem)._day).ToString();
            var month = (((Months)monthCombo.SelectedItem)._month).ToString();
            var year = (((Years)yearCombo.SelectedItem)._year).ToString();
            if (Convert.ToInt16(day) < 10)
            {
                day = "0" + day;
            }
            if (Convert.ToInt16(month) < 10)
            {
                month = "0" + month;
            }

            return day + "." + month + "." + year;
        }
    }

    public class Days
    {
        public int _day { get; set; }
        public string _weekday { get; set; }
    }

    public class Months
    {
        public int _month { get; set; }
        public string _monthname { get; set; }
    }

    public class Years
    {
        public int _year { get; set; }
        public int _century { get; set; }
        public int _decade { get; set; }
    }

    public class Source
    {
        private ObservableCollection<Days> _Days = new ObservableCollection<Days>();
        private ObservableCollection<Months> _Months = new ObservableCollection<Months>();
        private ObservableCollection<Years> _Years = new ObservableCollection<Years>();

        public ObservableCollection<Days> Days
        {
            get
            {
                return this._Days;
            }
        }

        public ObservableCollection<Months> Months
        { get { return this._Months; } }

        public ObservableCollection<Years> Years
        { get { return this._Years; } }

        public void GetDays(int m, int y)
        {
            for (int i = 1; i < DateTime.DaysInMonth(y, m) + 1; i++)
            {
                Days d = new Days();
                DateTime date = new DateTime(y, m, i);
                d._day = i;
                d._weekday = date.DayOfWeek.ToString().ToUpper();
                this._Days.Add(d);
            }
        }

        public void GetMonths()
        {
            for (int i = 1; i < 13; i++)
            {
                Months m = new Months();
                m._month = i;
                m._monthname = Convert.ToDateTime(i.ToString() + "." + i.ToString() + ".2000").ToString("MMMMMMMMMM").ToUpper();
                this._Months.Add(m);
            }
        }

        public void GetYears()
        {
            for (int i = 2012; i < 2025; i++)
            {
                Years y = new Years();
                y._year = i;
                y._century = Convert.ToInt32(i.ToString().Substring(0, 2));
                y._decade = Convert.ToInt32(i.ToString().Substring(2, 2));
                this._Years.Add(y);
            }
        }

    }
}
