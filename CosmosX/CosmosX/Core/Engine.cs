using CosmosX.Core.Contracts;
using CosmosX.IO;
using CosmosX.IO.Contracts;
using System;

namespace CosmosX.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = true;
        }

        public void Run()
        {
            while (isRunning)
            {
                var input = this.reader.ReadLine();

                var arguments = input.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

                var result = this.commandParser.Parse(arguments);

                this.writer.WriteLine(result);

                if (input == "Exit")
                {
                    this.isRunning = false;

                    this.writer.WriteLine(input);

                    return;
                }
            }
        }
    }
}