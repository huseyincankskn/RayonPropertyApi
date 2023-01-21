using Helper;
using System;
using System.Data;

namespace Core.Extensions
{
    public static class DataTableExtensions
    {
        /// <param name="format">string değer gelirse verilen formata göre parse eder.</param> 
        public static DateTime GetFieldAsDateTime(this DataRow dataRow, int columnIndex, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var value = dataRow.Field<object>(columnIndex);
            if (value == null)
            {
                return DateTime.MinValue;
            }

            if (value is DateTime dateTime)
            {
                return dateTime;
            }

            if (DateTime.TryParseExact(value.ToString(), format, null, System.Globalization.DateTimeStyles.None, out var dateTimeValue))
            {
                return dateTimeValue;
            }

            return DateTime.MinValue;
        }

        public static decimal GetFieldAsDecimal(this DataRow dataRow, int columnIndex)
        {
            var value = dataRow.Field<object>(columnIndex);
            if (value is decimal decimalValue)
            {
                return decimalValue;
            }

            if (value is string stringValue)
            {
                return stringValue.ToDecimal();
            }

            return Convert.ToDecimal(value);
        }

        public static string GetFieldAsString(this DataRow dataRow, int columnIndex)
        {
            var value = dataRow.Field<object>(columnIndex);
            if (value == null)
            {
                return string.Empty;
            }
            if (value is string stringValue)
            {
                return stringValue;
            }

            return value.ToString();
        }

        public static int GetFieldAsInteger(this DataRow dataRow, int columnIndex)
        {
            var value = dataRow.Field<object>(columnIndex);

            if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }
    }
}