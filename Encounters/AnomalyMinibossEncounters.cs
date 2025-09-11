using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class AnomalyMinibossEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("AnomalyMiniboss_Sign", ResourceLoader.LoadSprite("AbandonedAltarTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API anomalyMinibossEncounter = new EnemyEncounter_API(0, "H_Zone02_AnomalyMiniboss_Hard_EnemyBundle", "AnomalyMiniboss_Sign")
            {
                MusicEvent = "event:/Music/Mx_Spoggle",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            anomalyMinibossEncounter.CreateNewEnemyEncounterData(
                [
                    "AbandonedAltar_EN",
                ], [2]);
            anomalyMinibossEncounter.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_AnomalyMiniboss_Hard_EnemyBundle", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Hard);
        }
    }
}
