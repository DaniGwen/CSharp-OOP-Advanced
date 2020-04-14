
using System;
using System.Linq;
namespace FestivalManager.Core
{
    using System.Reflection;
    using Contracts;
    using Controllers;
    using Controllers.Contracts;
    using IO.Contracts;

    class Engine : IEngine
    {
        public IReader reader;
        public IWriter writer;

        private readonly IFestivalController festivalCоntroller;
        private readonly ISetController setCоntroller;
        private bool IsRunning;

        public Engine()
        {
            this.festivalCоntroller = new FestivalController();
            this.setCоntroller = new SetController();
            this.IsRunning = true;
        }

        public void Run()
        {
            while (IsRunning)
            {
                var input = this.reader.ReadLine();

                ProcessCommand(input);

                if (input == "END")
                {
                    this.IsRunning = false;
                    this.writer.WriteLine(input);
                }
            }
        }
        public string ProcessCommand(string input)
        {
            var args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var command = args[0];
            var arguments = args.Skip(1).ToList();

            //TODO
        }
    }
}