using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CoruscatingJumbleGutsEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("RainbowGuts_Sign", ResourceLoader.LoadSprite("RainbowGutsTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API rainbowGutsMedium = new EnemyEncounter_API(0, "H_Zone02_CoruscatingJumbleGuts_Medium_EnemyBundle", "RainbowGuts_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_JumbleGuts_Flummoxing_Medium_EnemyBundle")._roarReference.roarEvent,
            };
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "JumbleGuts_Hollowing_EN",
                "MusicMan_EN",
            ], null);
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "JumbleGuts_Flummoxing_EN",
                "MusicMan_EN",
            ], null);
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "JumbleGuts_Flummoxing_EN",
                "JumbleGuts_Clotted_EN",
            ], null);
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "JumbleGuts_Hollowing_EN",
                "Spoggle_Writhing_EN",
            ], null);
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "MusicMan_EN",
                "MusicMan_EN",
                "SingingStone_EN",
            ], null);
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "MusicMan_EN",
                "Scrungie_EN",
            ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                rainbowGutsMedium.CreateNewEnemyEncounterData(
                [
                    "CoruscatingJumbleGuts_EN",
                    "ColophonDelighted_EN",
                    "MusicMan_EN",
                ], null);
                rainbowGutsMedium.CreateNewEnemyEncounterData(
                [
                    "CoruscatingJumbleGuts_EN",
                    "ColophonDelighted_EN",
                    "MusicMan_EN",
                    "SingingStone_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                if (AApocrypha.CrossMod.Colophons)
                {
                    rainbowGutsMedium.CreateNewEnemyEncounterData(
                    [
                        "CoruscatingJumbleGuts_EN",
                        "SingingStone_EN",
                        "ColophonDisaffected_EN",
                    ], null);
                }
                if (AApocrypha.CrossMod.pigmentGilded)
                {
                    rainbowGutsMedium.CreateNewEnemyEncounterData(
                    [
                        "CoruscatingJumbleGuts_EN",
                        "AffluentJumbleguts_EN",
                        "MusicMan_EN",
                    ], null);
                }
            }
            rainbowGutsMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_CoruscatingJumbleGuts_Medium_EnemyBundle", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
