using RobotService.Core.Contracts;
using RobotService.Models.Garages;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Procedures;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly IGarage garage;
        private readonly Chip chip;
        private readonly TechCheck techCheck;
        private readonly Rest rest;
        private readonly Work work;
        private readonly Charge charge;
        private readonly Polish polish;
        private readonly IProcedure procedure;

        public Controller()
        {
            this.polish = new Polish();
            this.charge = new Charge();
            this.work = new Work();
            this.rest = new Rest();
            this.garage = new Garage();
            this.chip = new Chip();
            this.techCheck = new TechCheck();
        }

        public string Charge(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            this.charge.DoService(robot, procedureTime);

            return $"{robot.Name} had charge procedure";
        }

        public string Chip(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            chip.DoService(robot, procedureTime);

            return $"{robot.Name} had chip procedure";
        }

        //????
        public string History(string procedureType)
        {
            var result = string.Empty;

            switch (procedureType)
            {
                case "TechCheck":
                    result = this.techCheck.History();
                    break;
                case "Chip":
                    result = this.chip.History();
                    break;
                case "Rest":
                    result = this.rest.History();
                    break;
                case "Work":
                    result = this.work.History();
                    break;
                case "Charge":
                    result = this.charge.History();
                    break;
                case "Polish":
                    result = this.polish.History();
                    break;
            }
            return result;
        }

        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {
            try
            {
                var type = Assembly.GetExecutingAssembly()
               .GetTypes()
               .FirstOrDefault(t => t.Name == robotType);

                var robot = (IRobot)Activator
                    .CreateInstance(type, new object[] { name, energy, happiness, procedureTime });

                this.garage.Manufacture(robot);

                return $"Robot {robot.Name} registered successfully";
            }
            catch (Exception)
            {
                return ($"{robotType} type doesn't exist");
            }
        }

        public string Polish(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            this.polish.DoService(robot, procedureTime);

            return $"{robot.Name} had polish procedure";
        }

        public string Rest(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            this.rest.DoService(robot, procedureTime);

            return $"{robot.Name} had rest procedure";
        }

        public string Sell(string robotName, string ownerName)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            garage.Sell(robotName, ownerName);

            if (robot.IsChipped)
            {
                return $"{ownerName} bought robot with chip";
            }
            else
            {
                return $"{ownerName} bought robot without chip";
            }
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            this.techCheck.DoService(robot, procedureTime);

            return $"{robot.Name} had tech check procedure";
        }

        public string Work(string robotName, int procedureTime)
        {
            if (this.CheckRobotExist(robotName))
            {
                return $"Robot {robotName} does not exist";
            }

            var robot = this.garage.Robots[robotName];

            this.work.DoService(robot, procedureTime);

            return $"{robot.Name} was working for {procedureTime} hours";
        }

        private bool CheckRobotExist(string robotName)
        {
            if (!this.garage.Robots.ContainsKey(robotName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
