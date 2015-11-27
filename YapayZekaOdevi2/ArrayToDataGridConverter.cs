using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace YapayZekaOdevi2
{
    public class ArrayToDataGridConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = values[0] as Board;
            var myDataTable = new DataTable();

            List<byte> aa = board.BoardList.ToList();
            aa.Add(0);

            myDataTable.Columns.Add("0");
            myDataTable.Columns.Add("1");
            myDataTable.Columns.Add("2");
            myDataTable.Columns.Add("3");
            myDataTable.Columns.Add("4");

            for (int i = 0; i < 5; i++)
            {
                DataRow row = myDataTable.NewRow();

                row["0"] = aa[i * 5];
                row["1"] = aa[i * 5 + 1];
                row["2"] = aa[i * 5 + 2];
                row["3"] = aa[i * 5 + 3];
                row["4"] = aa[i * 5 + 4];

                myDataTable.Rows.Add(row);
            }

            return myDataTable.DefaultView;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
