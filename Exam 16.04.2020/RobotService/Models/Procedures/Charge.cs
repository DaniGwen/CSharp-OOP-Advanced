using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models.Procedures
{
    public class Charge : Procedure
    {
        public new void DoService(IRobot robot, int procedureTime)
        {
            if (robot.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("Robot doesn't have enough procedure time");
            }

            robot.ProcedureTime -= procedureTime;
            robot.Happiness += 12;
            robot.Energy += 10;

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
