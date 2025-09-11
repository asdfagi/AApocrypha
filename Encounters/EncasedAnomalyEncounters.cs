using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class EncasedAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("EncasedAnomaly_Sign", ResourceLoader.LoadSprite("EncasedAnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API encasedAnomalyMedium = new EnemyEncounter_API(0, "H_Zone02_EncasedAnomaly_Medium_EnemyBundle", "EncasedAnomaly_Sign")
            {
                MusicEvent = "event:/Music/Mx_Spoggle",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Spoggle_Ruminating_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
            encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            encasedAnomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_EncasedAnomaly_Medium_EnemyBundle", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
