using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class UnboundAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Anomaly_Sign", ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API anomalyEasy = new EnemyEncounter_API(0, "H_Zone02_UnboundAnomaly_Easy_EnemyBundle", "Anomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                ], null);
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "MusicMan_EN",
                ], null);
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "SingingStone_EN",
                ], null);
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "JumbleGuts_Hollowing_EN",
                    "MusicMan_EN",
                ], null);
            anomalyEasy.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            anomalyEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_UnboundAnomaly_Easy_EnemyBundle", 3, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API anomalyMedium = new EnemyEncounter_API(0, "H_Zone02_UnboundAnomaly_Medium_EnemyBundle", "Anomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            anomalyMedium.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "JumbleGuts_Hollowing_EN",
                    "SingingStone_EN",
                ], null);
            anomalyMedium.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                ], null);
            anomalyMedium.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "Spoggle_Resonant_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                anomalyMedium.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "ColophonDelighted_EN",
                ], null);
                anomalyMedium.CreateNewEnemyEncounterData(
                [
                    "UnboundAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    anomalyMedium.CreateNewEnemyEncounterData(
                    [
                        "UnboundAnomaly_EN",
                        "UnboundAnomaly_EN",
                        "ColophonDisaffected_EN",
                    ], null);
                }
            }
            anomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_UnboundAnomaly_Medium_EnemyBundle", 4, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
