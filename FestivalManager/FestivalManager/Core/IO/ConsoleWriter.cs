using FestivalManager.Core.IO.Contracts;
using System;
using System.Text;

namespace FestivalManager.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        private StringBuilder stringBuilder;
        public ConsoleWriter()
        {
            this.stringBuilder = new StringBuilder();
        }
        public void Write(string content)
        {
            stringBuilder.Append(content);
        }

        public void WriteLine(string content)
        {
            if (content != "END")
            {
                stringBuilder.AppendLine(content);
                return;
            }

            Console.WriteLine(stringBuilder.ToString().TrimEnd());
        }
    }
}