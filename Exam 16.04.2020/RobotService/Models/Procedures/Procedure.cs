using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models.Procedures
{
    public abstract class Procedure : IProcedure
    {
        protected readonly Dictionary<string, List<IRobot>> robots;

        public Procedure()
        {
            this.robots = new Dictionary<string, List<IRobot>>();
        }

        public void DoService(IRobot robot, int procedureTime)
        {
        }

        //?????
        public string History()
        {
            var result = new StringBuilder();

            result.AppendLine($"{this.GetType().Name}");

            foreach (var robot in this.robots[this.GetType().Name])
            {
                result
                    .AppendLine($" Robot type: {robot.GetType().Name} - {robot.Name} - Happiness: {robot.Happiness} - Energy: {robot.Energy}");
            }

            return result.ToString().TrimEnd('\r', '\n');
        }
    }
}
