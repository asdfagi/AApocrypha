using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TearDrinkerEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("TearDrinker_Sign", ResourceLoader.LoadSprite("TearDrinkerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API tearDrinkerEasy = new EnemyEncounter_API(0, "H_Zone01_TearDrinker_Easy_EnemyBundle", "TearDrinker_Sign")
            {
                MusicEvent = "event:/AAMusic/YellowFrog",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Keko/CHR_ENM_Keko_Roar",
            };
            tearDrinkerEasy.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                ], null);
            tearDrinkerEasy.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "Mung_EN",
                ], null);
            tearDrinkerEasy.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            tearDrinkerEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_TearDrinker_Easy_EnemyBundle", 4, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);
            
            EnemyEncounter_API tearDrinkerMedium = new EnemyEncounter_API(0, "H_Zone01_TearDrinker_Medium_EnemyBundle", "TearDrinker_Sign")
            {
                MusicEvent = "event:/AAMusic/YellowFrog",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Keko/CHR_ENM_Keko_Roar",
            };
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "TearDrinker_EN",
                ], null);
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "TearDrinker_EN",
                    "MudLung_EN",
                ], null);
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "MudLung_EN",
                    "MudLung_EN",
                ], null);
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "MunglingMudLung_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "JumbleGuts_Clotted_EN",
                    "JumbleGuts_Waning_EN",
                ], null);
            tearDrinkerMedium.CreateNewEnemyEncounterData(
                [
                    "TearDrinker_EN",
                    "JumbleGuts_Clotted_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "ColophonComposed_EN",
                        "JumbleGuts_Clotted_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "ColophonDefeated_EN",
                        "JumbleGuts_Waning_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "ColophonDefeated_EN",
                        "Spoggle_Ruminating_EN",
                    ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "Goomba_EN",
                        "JumbleGuts_Clotted_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "TearDrinker_EN",
                        "Spoggle_Ruminating_EN",
                        "Follower_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "JumbleGuts_Clotted_EN",
                        "MycotoxicSpoggle_EN",
                    ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "TearDrinker_EN",
                        "NobodyGrave_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "MudLung_EN",
                        "Minana_EN",
                        "Mung_EN",
                    ], null);
                tearDrinkerMedium.CreateNewEnemyEncounterData(
                    [
                        "TearDrinker_EN",
                        "TearDrinker_EN",
                        "LittleBeak_EN",
                    ], null);
            }
            tearDrinkerMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_TearDrinker_Medium_EnemyBundle", 14, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
