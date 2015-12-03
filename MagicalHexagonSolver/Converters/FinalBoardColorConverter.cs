using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MagicalHexagonSolver.Models;

namespace MagicalHexagonSolver.Converters
{
    public class FinalBoardColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = value as Board;

            return board.IsFinalBoard ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
