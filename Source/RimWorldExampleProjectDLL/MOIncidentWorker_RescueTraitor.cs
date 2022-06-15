using RimWorld;
using Verse;

namespace MoreIncidents;

public class MOIncidentWorker_RescueTraitor : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        var intVec = CellFinderLoose.RandomCellWith(sq => sq.Standable(map) && !map.fogGrid.IsFogged(sq), map);
        var thing = ThingMaker.MakeThing(ThingDef.Named("MO_RTWorker"));
        GenSpawn.Spawn(thing, intVec, map);
        return true;
    }
}