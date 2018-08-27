using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IStationRepository
    {
        List<Station> GetCurrentStationsState();
        void AddStation(Station station);
    }
}
