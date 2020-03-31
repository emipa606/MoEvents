using System;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_CalmWeather : IncidentWorker
	{
		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			map.weatherManager.TransitionTo(WeatherDef.Named("Clear"));
			map.weatherManager.curWeather.moveSpeedMultiplier = 1f;
			map.weatherManager.curWeather.windSpeedFactor = 0f;
			map.weatherManager.curWeather.accuracyMultiplier = 2f;
			map.weatherManager.curWeather.rainRate = 0f;
			map.weatherManager.curWeather.snowRate = 0f;
			Find.LetterStack.ReceiveLetter("MO_CalmWeather".Translate(), "MO_CalmWeatherDesc".Translate(), LetterDefOf.NegativeEvent, null); //"The weather just got remarkably calm. Wind turbines will be useless, but shooting will be easier."
			return true;
		}
	}
}

