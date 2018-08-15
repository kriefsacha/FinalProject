using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Station
    {
        public int Number { get; set; }

        public Plane Plane { get; set; }

        public bool IsAvailable
        {
            get { return Plane == null; }
        }

    }
}
