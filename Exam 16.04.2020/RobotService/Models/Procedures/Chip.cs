using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models.Procedures
{
    public class Chip : Procedure
    {
        //?????
        public new void DoService(IRobot robot, int procedureTime)
        {
            foreach (var rob in base.robots)
            {
                if (base.robots[this.GetType().Name].Contains(robot))
                {
                    throw new ArgumentException($"{robot.Name} is already chipped");
                }
            }

            if (robot.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("Robot doesn't have enough procedure time");
            }

            robot.ProcedureTime -= procedureTime;
            robot.Happiness -= 5;
            robot.IsChipped = true;

            if (!base.robots.ContainsKey(this.GetType().Name))
            {
                base.robots.Add(this.GetType().Name, new List<IRobot>() { robot });
            }
            else
            {
                base.robots[this.GetType().Name].Add(robot);
            }
        }
    }
}
