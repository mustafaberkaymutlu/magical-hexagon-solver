using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace YapayZekaOdevi2
{
    public class ArrayToHexagonalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = value as Board;

            return board.BoardList[Int32.Parse((string)parameter)];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
