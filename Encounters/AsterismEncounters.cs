using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class AsterismEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Asterism_Sign", ResourceLoader.LoadSprite("AsterismTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API asterismMedium = new EnemyEncounter_API(0, "H_Zone01_Asterism_Medium_EnemyBundle", "Asterism_Sign")
            {
                MusicEvent = "event:/AAMusic/BeautifulButWrong",
                RoarEvent = "event:/Characters/Enemies/Spoggle_Purple/CHR_ENM_Spoggle_Purple_Roar",
            };
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Keko_EN",
                ], null);
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "MudLung_EN",
                    "Mung_EN",
                    "Mung_EN",
                ], null);
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Asterism_EN",
                ], null);
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Acolyte_EN",
                ], null);
            asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "SandSifter_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Flakkid_EN",
                ], null);
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Enno_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Minana_EN",
                    "Minana_EN",
                ], null);
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Pinano_EN",
                ], null);
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Wall_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Mung_EN",
                    "ColophonDefeated_EN",
                ], null);
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Mung_EN",
                    "ColophonComposed_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Scylla_EN",
                    "Mung_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Goomba_EN",
                ], null);
                asterismMedium.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "MycotoxicSpoggle_EN",
                    "Acolyte_EN",
                ], null);
            }
            asterismMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Asterism_Medium_EnemyBundle", 12, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API asterismHard = new EnemyEncounter_API(0, "H_Zone01_Asterism_Hard_EnemyBundle", "Asterism_Sign")
            {
                MusicEvent = "event:/AAMusic/BeautifulButWrong",
                RoarEvent = "event:/Characters/Enemies/Spoggle_Purple/CHR_ENM_Spoggle_Purple_Roar",
            };
            asterismHard.CreateNewEnemyEncounterData(
            [
                "Asterism_EN",
                "Asterism_EN",
                "Asterism_EN",
                "Asterism_EN",
            ], null);
            asterismHard.CreateNewEnemyEncounterData(
            [
                "Asterism_EN",
                "Asterism_EN",
                "Asterism_EN",
                "Spoggle_Ruminating_EN",
            ], null);
            asterismHard.CreateNewEnemyEncounterData(
            [
                "Asterism_EN",
                "Asterism_EN",
                "Asterism_EN",
                "Spoggle_Spitfire_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                asterismHard.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Follower_EN",
                    "Asterism_EN",
                ], null);
                asterismHard.CreateNewEnemyEncounterData(
                [
                    "Asterism_EN",
                    "Asterism_EN",
                    "Asterism_EN",
                    "MycotoxicSpoggle_EN",
                ], null);
            }
            ;
            asterismHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Asterism_Hard_EnemyBundle", 14, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard);
        }
    }
}
