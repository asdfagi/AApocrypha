using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SharpenedAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SharpenedAnomaly_Sign", ResourceLoader.LoadSprite("SharpenedAnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sharpenedAnomalyMedium = new EnemyEncounter_API(0, "H_Zone02_SharpenedAnomaly_Medium_EnemyBundle", "SharpenedAnomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "Spoggle_Ruminating_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "Spoggle_Spitfire_EN",
                    "Jumbleguts_Hollowing_EN",
                ], null);
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "DevotedSpoggle_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "CellularSpoggle_EN",
                    "Jumbleguts_Waning_EN",
                ], null);
            sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "ColophonMaladjusted_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                    [
                        "SharpenedAnomaly_EN",
                        "ColophonDisaffected_EN",
                        "CellularSpoggle_EN",
                    ], null);
                }
                if (AApocrypha.CrossMod.pigmentRainbow)
                {
                    sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                    [   
                        "SharpenedAnomaly_EN",
                        "ColophonMaladjusted_EN",
                        "CoruscatingJumbleGuts_EN",
                    ], null);
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks) 
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "BackupDancer_EN",
                    "MusicMan_EN",
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "EncasedAnomaly_EN",
                    "Frostbite_Bipedal_EN",
                    "Frostbite_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "YellowFlower_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "Enigma_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "RedBot_EN",
                    "MusicMan_EN",
                ], null);
                if (AApocrypha.CrossMod.EnemyPack)
                {
                    sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                    [
                        "SharpenedAnomaly_EN",
                        "YellowBot_EN",
                        "Neoplasm_EN",
                        "Neoplasm_EN",
                    ], null);
                }
            }
            if (AApocrypha.CrossMod.IntoTheAbyss && AApocrypha.CrossMod.pigmentGilded)
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "AffluentJumbleguts_EN",
                    "CellularSpoggle_EN"
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "AffluentJumbleguts_EN",
                    "DevotedSpoggle_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "MusicMan_EN",
                    "Hagwitch_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "Spoggle_Spitfire_EN",
                    "CoruscatingJumbleGuts_EN",
                ], null);
                sharpenedAnomalyMedium.CreateNewEnemyEncounterData(
                [
                    "SharpenedAnomaly_EN",
                    "CellularSpoggle_EN",
                    "CoruscatingJumbleGuts_EN",
                ], null);
            }
            sharpenedAnomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_SharpenedAnomaly_Medium_EnemyBundle", 15, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
