using System;
using RimWorld.Planet;
using Verse;

namespace MoreIncidents.Settings
{
    // Token: 0x0200000F RID: 15
    internal class WorldComp : WorldComponent
    {
        public WorldComp(World world) : base(world)
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            Log.Message("Mo Events - Settings loaded", false);
            ME_ModSettings.ChangeDefPost();
        }
    }
}
