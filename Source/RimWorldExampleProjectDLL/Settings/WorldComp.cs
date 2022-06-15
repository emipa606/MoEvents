using RimWorld.Planet;
using Verse;

namespace MoreIncidents.Settings;

internal class WorldComp : WorldComponent
{
    public WorldComp(World world) : base(world)
    {
    }

    public override void FinalizeInit()
    {
        base.FinalizeInit();
        Log.Message("Mo Events - Settings loaded");
        ME_ModSettings.ChangeDefPost();
    }
}