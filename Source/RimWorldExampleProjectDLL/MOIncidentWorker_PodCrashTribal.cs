using System;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_PodCrashTribal : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
            Faction faction = Find.FactionManager.RandomNonHostileFaction(true, true, true, TechLevel.Neolithic);
            PawnGenerationRequest pawnGenerationRequest = new PawnGenerationRequest(PawnKindDefOf.Villager, faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
                    null, 1f, null, null, null, null, null, null);
            Pawn pawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);
			HealthUtility.DamageUntilDowned(pawn);
			string text = "MO_TribalAid".Translate();
			string text2 = "MO_TribalAidDesc".Translate(); //"One of the tribes has send you a message.\n\nWhile the language of the tribals was unfamiliar, the intention was clear: The tribal party didn't have the ability to treat the injuries of one of their friends and they pleaded that you heal their friend..\n\nYou can help the injured tribal to improve relations with the corresponding faction, or capture them for slavery or recruitment purposes.";
			Find.LetterStack.ReceiveLetter(text, text2, LetterDefOf.NewQuest, new TargetInfo(intVec, map, false), null);
			DropPodUtility.MakeDropPodAt(intVec, map, new ActiveDropPodInfo
			{
				SingleContainedThing = pawn,
				openDelay = 180,
				leaveSlag = true
			});
			return true;
		}

		private const float FogClearRadius = 4.5f;

		private const float RelationWithColonistWeight = 20f;
	}
}

