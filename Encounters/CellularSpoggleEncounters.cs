using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CellularSpoggleEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("CellularSpoggle_Sign", ResourceLoader.LoadSprite("CellularSpoggleTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API cellularSpoggleMedium = new EnemyEncounter_API(0, "H_Zone02_CellularSpoggle_Medium_EnemyBundle", "CellularSpoggle_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-WhimperAndWhine",
                RoarEvent = "event:/Characters/Enemies/WrithingSpoggle/CHR_ENM_WrithingSpoggle_Roar",
            };
            cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Spoggle_Spitfire_EN",
                ], null);
            cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "DevotedSpoggle_EN",
                ], null);
            cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Scrungie_EN",
                ], null);
            cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "ColophonDelighted_EN",
                    "SingingStone_EN",
                ], null);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    cellularSpoggleMedium.CreateNewEnemyEncounterData(
                    [
                        "CellularSpoggle_EN",
                        "ColophonDisaffected_EN",
                    ], null);
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Jansuli_EN",
                    "MusicMan_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "NakedGizo_EN",
                ], null);
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Chapman_EN",
                    "Chapman_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Rabies_EN",
                ], null);
                cellularSpoggleMedium.CreateNewEnemyEncounterData(
                [
                    "CellularSpoggle_EN",
                    "Spoggle_Spitfire_EN",
                    "BlueBot_EN",
                ], null);
            }
            cellularSpoggleMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_CellularSpoggle_Medium_EnemyBundle", 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
