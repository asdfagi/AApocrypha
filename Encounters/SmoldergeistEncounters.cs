using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SmoldergeistEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Smoldergeist_Sign", ResourceLoader.LoadSprite("SmoldergeistTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API smoldergeistHard = new EnemyEncounter_API(0, "H_Zone01_Smoldergeist_Hard_EnemyBundle", "Smoldergeist_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/Sodom&Gomorrah",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Spoggle_Writhing_Medium_EnemyBundle")._roarReference.roarEvent,
            };
            smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MudLung_EN",
                    "Mung_EN",
                    "Mung_EN",
                ], null);
            smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "Keko_EN",
                    "Keko_EN",
                ], null);
            smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MunglingMudLung_EN",
                    "Spoggle_Spitfire_EN",
                ], null);
            smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MunglingMudLung_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "FlaMinGoa_EN",
                    "MudLung_EN",
                ], null);
            if (AApocrypha.CrossMod.Mythos)
            {
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MudLung_EN",
                    "Madman_EN",
                ], null);
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MunglingMudLung_EN",
                    "RatThing_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "MudLung_EN",
                    "Keklung_EN",
                ], null);
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "Flakkid_EN",
                    "DryBait_EN",
                ], null);
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "Flakkid_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "Enno_EN",
                    "Enno_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                smoldergeistHard.CreateNewEnemyEncounterData(
                [
                    "Smoldergeist_EN",
                    "Snaurce_EN",
                    "MudLung_EN",
                ], null);
            }
            smoldergeistHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Smoldergeist_Hard_EnemyBundle", 9, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard);
        }
    }
}
