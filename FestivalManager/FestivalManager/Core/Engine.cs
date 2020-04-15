
using System;
using System.Linq;
using System.Reflection;
using FestivalManager.Core.Contracts;
using FestivalManager.Core.Controllers;
using FestivalManager.Core.Controllers.Contracts;
using FestivalManager.Core.IO;
using FestivalManager.Core.IO.Contracts;
using FestivalManager.Entities;
using FestivalManager.Entities.Contracts;

namespace FestivalManager.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IFestivalController festivalCоntroller;
        private readonly ISetController setCоntroller;
        private readonly IStage stage;

        private bool IsRunning;

        public Engine()
        {
            this.stage = new Stage();
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
            this.festivalCоntroller = new FestivalController(this.stage);
            this.setCоntroller = new SetController(this.stage);
            this.IsRunning = true;
        }

        public void Run()
        {
            while (IsRunning)
            {
                var input = this.reader.ReadLine();

                var result = this.ProcessCommand(input);

                this.writer.WriteLine(result);

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
            var arguments = args.Skip(1).ToArray();

            var result = string.Empty;

            if (command == "LetsRock")
            {
                result = this.setCоntroller.PerformSets();
            }
            else if (command == "END")
            {
                result = this.festivalCоntroller.ProduceReport();
            }
            else
            {
                result = (string)typeof(IFestivalController)
                              .GetMethods()
                              .FirstOrDefault(m => m.Name == command)
                              .Invoke(this.festivalCоntroller, new object[] { arguments });
            }

            return result;
        }
    }
}