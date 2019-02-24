using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Common
{
    public static class CommonUtility
    {
        public static string GetMySQLDateTime(string dateTime, DateDataType dateType)
        {
            string date = dateTime;
            if (date.Trim() == "") return null;
            try
            {
                //DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
                //dateTimeFormatInfo.TimeSeparator = DateFormatInfo.TimeSeparator;
                DateTime dt = DateTime.Parse(date);
                switch (dateType)
                {
                    case DateDataType.Date:
                        date = dt.ToString(DateFormatInfo.MySQLFormat.DateUpdate);
                        break;
                    case DateDataType.DateTime:
                        date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeUpdate);
                        break;
                    case DateDataType.TimeStamp:
                        date = dt.ToString(DateFormatInfo.MySQLFormat.TimeStampUpdate);
                        break;
                    case DateDataType.Time:
                        date = dt.ToString(DateFormatInfo.TimeFormat);
                        break;
                    case DateDataType.DateNoFormatBegin:
                        date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatBegin);
                        break;
                    case DateDataType.DateNoFormatEnd:
                        date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatEnd);
                        break;
                }
            }
            catch (Exception) { }

            return date;
        }
    }

    public enum DateDataType
    {
        Date,
        DateTime,
        Time,
        TimeStamp,
        DateNoFormatBegin,
        DateNoFormatEnd
    }
}
