using System;
using System.Globalization;
using System.Windows.Data;

namespace Alexandria.Client.Infrastructure
{
    public class TimeSpanToHumanReadableStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var ts = (TimeSpan)value;
			if (ts.TotalDays > 30)
			{
				return (ts.TotalDays / 30f).ToString("0.#") + " months";
			}
			if (ts.TotalDays > 7)
			{
				return (ts.TotalDays/7f).ToString("0.#") + " weeks";
			}
			if(ts.TotalDays>1)
			{
				return (ts.TotalDays).ToString("0.#") + " days";
			}
			return ts.TotalHours.ToString("0.#") + " hours";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}