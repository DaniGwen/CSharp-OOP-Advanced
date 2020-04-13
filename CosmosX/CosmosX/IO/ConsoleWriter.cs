using System;
using System.IO;
using System.Text;
using CosmosX.IO.Contracts;

namespace CosmosX.IO
{
    public class ConsoleWriter : IWriter
    {
        private StringBuilder sb;

        public ConsoleWriter()
        {
            this.sb = new StringBuilder();
        }

        public void WriteLine(string result)
        {
            if (result == "Exit")
            {
                Console.WriteLine(this.sb.ToString().TrimEnd());
                return;
            }
            else
            {
                sb.AppendLine(result);
            }
            // System.Console.WriteLine(result);
        }
    }
}