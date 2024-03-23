using RimWorld.Planet;
using Verse;

namespace MoreIncidents.Settings;

internal class WorldComp(World world) : WorldComponent(world)
{
    public override void FinalizeInit()
    {
        base.FinalizeInit();
        Log.Message("Mo Events - Settings loaded");
        ME_ModSettings.ChangeDefPost();
    }
}