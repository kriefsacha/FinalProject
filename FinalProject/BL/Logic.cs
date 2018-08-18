using Common;
using Common.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Logic : ILogic
    {
        Manager manager;
        public Logic()
        {
            manager = new Manager();
        }
        public void Arrival(Plane Plane)
        {
            manager.Arrival(Plane);
        }

        public void Departure(Plane Plane)
        {
            manager.Departure(Plane);
        }

        public void Init()
        {
            
        }

        public void MoveRequest(Plane Plane)
        {
           
        }
    }
}
