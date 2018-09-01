using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IStationRepository
    {
        List<Station> GetCurrentStationsState();
        List<Relation> GetRelations();
    }
}
