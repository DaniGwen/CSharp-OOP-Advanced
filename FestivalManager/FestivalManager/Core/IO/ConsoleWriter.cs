using FestivalManager.Core.IO.Contracts;
using System;
using System.Text;

namespace FestivalManager.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        private StringBuilder sb;
        public ConsoleWriter()
        {
            this.sb = new StringBuilder();
        }
        public void Write(string content)
        {
            sb.Append(content);
        }

        public void WriteLine(string content)
        {
            if (content != "END")
            {
                sb.AppendLine(content);
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}