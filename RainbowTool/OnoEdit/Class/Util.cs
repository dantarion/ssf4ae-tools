using System;
using System.Windows;
using System.Windows.Controls;

namespace OnoEdit.Class
{
    class Util
    {
        public static Size MeasureString(string s, int offset)
        {

            if (string.IsNullOrEmpty(s))
            {
                return new Size(0, 0);
            }

            var textBlock = new TextBlock()
            {
                Text = s,
                FontSize = 16
            };

            textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));            
            return new Size(textBlock.DesiredSize.Width + offset, textBlock.DesiredSize.Height);
        }
    }
}
