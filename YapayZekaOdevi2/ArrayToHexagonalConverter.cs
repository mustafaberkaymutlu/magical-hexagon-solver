using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YapayZekaOdevi2
{
    public class ArrayToHexagonalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Board board = value as Board;

            List<List<byte>> aa = new List<List<byte>>();
            aa.Add(new List<byte> { board.BoardList[0], board.BoardList[1], board.BoardList[2] });
            aa.Add(new List<byte> { board.BoardList[3], board.BoardList[4], board.BoardList[5], board.BoardList[6] });
            aa.Add(new List<byte> { board.BoardList[7], board.BoardList[8], board.BoardList[9], board.BoardList[10], board.BoardList[11] });
            aa.Add(new List<byte> { board.BoardList[12], board.BoardList[13], board.BoardList[14], board.BoardList[15] });
            aa.Add(new List<byte> { board.BoardList[16], board.BoardList[17], board.BoardList[18] });

            return aa;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
