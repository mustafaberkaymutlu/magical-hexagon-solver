using System;
using System.Globalization;
using System.Windows.Data;
using MagicalHexagonSolver.Models;

namespace MagicalHexagonSolver.Converters
{
    public class ArrayToHexagonalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = value as Board;

            return board.BoardList[int.Parse((string)parameter)];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
