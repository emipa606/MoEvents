using System;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_RescueTraitor : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			IntVec3 intVec = CellFinderLoose.RandomCellWith((IntVec3 sq) => GenGrid.Standable(sq, map) && !map.fogGrid.IsFogged(sq), map, 1000);
			Thing thing = ThingMaker.MakeThing(ThingDef.Named("MO_RTWorker"), null);
			GenSpawn.Spawn(thing, intVec, map);
			return true;
		}
	}
}

