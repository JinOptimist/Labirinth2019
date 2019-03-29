using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLabirinth.Draw
{
    public class ConsoleStringBuilder
    {
        private StringBuilder sb = new StringBuilder();

        private List<Tuple<string, ConsoleColor>> coloredLines = new List<Tuple<string, ConsoleColor>>();

        private ConsoleColor lastUsedColor;

        public void AppendLine(string str = "", ConsoleColor? color = null)
        {
            Append(str + "\r\n", color);
        }

        public void Append(char chr, ConsoleColor? color = null)
        {
            Append(chr.ToString(), color);
        }

        public void Append(string str, ConsoleColor? color = null)
        {
            if (!color.HasValue)
            {
                color = Console.ForegroundColor;
            }

            if (lastUsedColor == color)
            {
                sb.Append(str);
            }
            else
            {
                coloredLines.Add(new Tuple<string, ConsoleColor>(sb.ToString(), lastUsedColor));
                sb = new StringBuilder(str);
                lastUsedColor = color.Value;
            }
        }

        public void WriteToConsole()
        {
            coloredLines.Add(new Tuple<string, ConsoleColor>(sb.ToString(), lastUsedColor));

            var oldColor = Console.ForegroundColor;
            foreach (var colorLine in coloredLines)
            {
                Console.ForegroundColor = colorLine.Item2;
                Console.Write(colorLine.Item1);
            }
            Console.ForegroundColor = oldColor;
        }
    }
}
