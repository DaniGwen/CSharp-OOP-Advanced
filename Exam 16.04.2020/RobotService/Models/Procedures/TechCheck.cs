using RobotService.Models.Robots.Contracts;
using System;
using System.Collections.Generic;

namespace RobotService.Models.Procedures
{
   public class TechCheck : Procedure
    {
        //???
        public new void DoService(IRobot robot, int procedureTime)
        {
            if (robot.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("Robot doesn't have enough procedure time");
            }

            robot.ProcedureTime -= procedureTime;
            robot.Energy -= 8;
            robot.IsChecked = true;
            

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
