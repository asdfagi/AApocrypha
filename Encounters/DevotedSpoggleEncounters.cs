using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class DevotedSpoggleEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("DevotedSpoggle_Sign", ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API devotedSpoggleMedium = new EnemyEncounter_API(0, "H_Zone02_DevotedSpoggle_Medium_EnemyBundle", "DevotedSpoggle_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-WhimperAndWhine",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Spoggle_Resonant_Medium_EnemyBundle")._roarReference.roarEvent,
            };
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Acolyte_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Scrungie_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Acolyte_EN",
                    "UnboundAnomaly_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "DevotedSpoggle_EN",
                    "ManicMan_EN",
                    "ManicMan_EN",
                ], null);
            devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "ManicMan_EN",
                    "ManicMan_EN",
                    "ManicMan_EN",
                ], null);
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "ManicMan_EN",
                    "ManicMan_EN",
                    "CoruscatingJumbleGuts_EN",
                ], null);
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Scrungie_EN",
                    "CoruscatingJumbleGuts_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "ColophonDelighted_EN",
                ], null);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    devotedSpoggleMedium.CreateNewEnemyEncounterData(
                    [
                        "DevotedSpoggle_EN",
                        "ColophonDisaffected_EN",
                        "SingingStone_EN",
                    ], null);
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "MusicMan_EN",
                    "BackupDancer_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Pilgrim_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Rabies_EN",
                ], null);
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Spoggle_Ruminating_EN",
                    "RedBot_EN",
                ], null);
                devotedSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "DevotedSpoggle_EN",
                    "Something_EN",
                ], null);
            }
            devotedSpoggleMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_DevotedSpoggle_Medium_EnemyBundle", 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}