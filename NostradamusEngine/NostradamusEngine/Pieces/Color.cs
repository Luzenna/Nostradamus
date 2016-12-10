using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public enum Color
    {
        White,
        Black
    }

    public static class ColorHelper
    {
        public static Color Reverse(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }
    }

}
