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
                    "SharpenedAnomaly_EN",
                    "MusicMan_EN",
                    "Scrungie_EN",
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
            encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "MusicMan_EN",
                    "Scrungie_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            { 
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Spoggle_Writhing_EN",
                    "ColophonDelighted_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Spoggle_Ruminating_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "CellularSpoggle_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    encasedAnomalyMedium.CreateNewEnemyEncounterData(
                    [
                        "EncasedAnomaly_EN",
                        "Spoggle_Resonant_EN",
                        "ColophonDisaffected_EN",
                    ], null);
                    encasedAnomalyMedium.CreateNewEnemyEncounterData(
                    [
                        "EncasedAnomaly_EN",
                        "Fanatic_EN",
                        "ColophonDelighted_EN",
                    ], null);
                }
            };
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Spoggle_Resonant_EN",
                    "Follower_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "DevotedSpoggle_EN",
                    "Follower_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Fanatic_EN",
                    "Follower_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "Jansuli_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Thunderdome_EN",
                    "Spoggle_Resonant_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "UnboundAnomaly_EN",
                    "Moone_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Moone_EN",
                    "Scrungie_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "Chapman_EN",
                    "Chapman_EN",
                ], null);
                encasedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "EncasedAnomaly_EN",
                    "SharpenedAnomaly_EN",
                    "NakedGizo_EN",
                ], null);
            }
            encasedAnomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_EncasedAnomaly_Medium_EnemyBundle", 15, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
