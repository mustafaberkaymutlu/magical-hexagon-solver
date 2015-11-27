using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YapayZekaOdevi2
{
    public class ArrayToDataGridConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = values[0] as Board;
            //var myDataTable = ConvertToDatatable(board.BoardList);
            var myDataTable = new DataTable();

            myDataTable.Columns.Add("0");
            myDataTable.Columns.Add("1");
            myDataTable.Columns.Add("2");
            myDataTable.Columns.Add("3");
            myDataTable.Columns.Add("4");

            for (int i = 0; i < 5; i++)
            {
                DataRow row = myDataTable.NewRow();

                row["0"] = board.BoardList[i * 4];
                row["1"] = board.BoardList[i * 4 + 1];
                row["2"] = board.BoardList[i * 4 + 2];
                row["3"] = board.BoardList[i * 4 + 3];
                row["3"] = board.BoardList[i * 4 + 4];

                myDataTable.Rows.Add(row);
            }

            return myDataTable.DefaultView;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
