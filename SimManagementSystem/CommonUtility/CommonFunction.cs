using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SimManagementSystem.CommonUtility
{
    public class CommonFunction
    {
        public bool isDataTableContainRecords(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);

            }
            return data;
        }
        public  List<T> GetObjectList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItems<T>(row); 
                list.Add(item);
            }
            return list;
        }

        private static T GetItems<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo property in temp.GetProperties())
                {
                    if (property.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] != DBNull.Value)
                            property.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], pro.PropertyType), null);
                    else
                        continue;
                }
            }
            obj.CopyDatePropertiesToStringProperties();
            return obj;
        }
        public static string GetFormattedDate(string sDate)
        {
            string Result = "";
            if (isValidDate(sDate))
            {
                DateTime dt = Convert.ToDateTime(sDate);
                string Year = dt.Year.ToString();
                string Month = dt.Month.ToString();
                string Date = dt.Day.ToString();
                Month = GetMonthNameByNumber(Month);
                Result = Date + " " + Month + " " + Year;
            }

            return Result;
        }
        public static bool isValidDate(string Date)
        {
            if (string.IsNullOrEmpty(Date))
                Date = "";

            DateTime value;
            if (!DateTime.TryParse(Date, out value))
            {
                return false;
            }
            return true;

        }
        public static string GetMonthNameByNumber(string sNumber)
        {
            int Number = 0;

            if (isGreaterThanZeroInteger(sNumber))
                Number = Convert.ToInt32(sNumber);

            string MonthName = "";

            if (Number == 1)
                MonthName = "January";

            if (Number == 2)
                MonthName = "February";

            if (Number == 3)
                MonthName = "March";

            if (Number == 4)
                MonthName = "April";

            if (Number == 5)
                MonthName = "May";

            if (Number == 6)
                MonthName = "June";

            if (Number == 7)
                MonthName = "July";

            if (Number == 8)
                MonthName = "August";

            if (Number == 9)
                MonthName = "September";

            if (Number == 10)
                MonthName = "October";

            if (Number == 11)
                MonthName = "November";

            if (Number == 12)
                MonthName = "December";


            return MonthName;


        }
        public static bool isGreaterThanZeroInteger(string input)
        {
            if (string.IsNullOrEmpty(input))
                input = "";

            input = input.Trim();
            bool result = true;

            Int32 value;

            if (Int32.TryParse(input, out value) == false)
                result = false;
            else
            {
                int Temp = Convert.ToInt32(input);
                if (Temp < 1)
                    result = false;
            }

            return result;
        }
    }
    public static class ObjectExtensionMethods
    {
        public static void CopyDatePropertiesToStringProperties(this object self)
        {

            var toProperties = self.GetType().GetProperties();

            foreach (var toProperty in toProperties)
            {
                if (toProperty.PropertyType.Name == "DateTime")
                {
                    var property = toProperties.Where(x => x.Name == toProperty.Name + "String").FirstOrDefault();
                    var val = toProperty.GetValue(self);
                    if (val != null)
                    {
                        property.SetValue(self, CommonFunction.GetFormattedDate(Convert.ToDateTime(val).Date.ToShortDateString()));
                    }
                }
                if (toProperty.PropertyType.Name == "Nullable`1")
                {
                    var property = toProperties.Where(x => x.Name == toProperty.Name + "String").FirstOrDefault();
                    var val = toProperty.GetValue(self);
                    if (val != null)
                    {
                        property.SetValue(self, CommonFunction.GetFormattedDate(Convert.ToDateTime(val).Date.ToShortDateString()));
                    }
                }

            }
        }
    }
}