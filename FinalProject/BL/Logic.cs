using Common;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Logic : ILogic
    {
        IDAL manager;

        public Logic(IDAL DAL)
        {
            manager = DAL;
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
