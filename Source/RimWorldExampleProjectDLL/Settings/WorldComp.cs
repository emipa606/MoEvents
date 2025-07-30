using RimWorld.Planet;

namespace MoreIncidents.Settings;

internal class WorldComp(World world) : WorldComponent(world)
{
    public override void FinalizeInit(bool fromLoad)
    {
        base.FinalizeInit(fromLoad);
        ME_ModSettings.ChangeDefPost();
    }
}