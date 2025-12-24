using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class AcolyteFarShoreEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Acolyte_Sign", ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API acolyteEasy = new EnemyEncounter_API(0, "H_Zone01_Acolyte_Easy_EnemyBundle", "Acolyte_Sign")
            {
                MusicEvent = "event:/AAMusic/FinalFantasy/DarkTower",
                RoarEvent = "event:/Characters/Enemies/InHisImage/CHR_ENM_InHisImage_Roar",
            };
            acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                ], null);
            acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Mung_EN",
                ], null);
            acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Keko_EN",
                ], null);
            acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Mung_EN",
                    "Mung_EN",
                ], null);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Flakkid_EN",
                ], null);
                acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Enno_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Minana_EN",
                ], null);
                acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Pinano_EN",
                ], null);
                acolyteEasy.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Wall_EN",
                ], null);
            }
            acolyteEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Acolyte_Easy_EnemyBundle", 6, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API acolyteMedium = new EnemyEncounter_API(0, "H_Zone01_Acolyte_Medium_EnemyBundle", "Acolyte_Sign")
            {
                MusicEvent = "event:/AAMusic/FinalFantasy/DarkTower",
                RoarEvent = "event:/Characters/Enemies/InHisImage/CHR_ENM_InHisImage_Roar",
            };
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "MudLung_EN",
                    "MudLung_EN",
                ], null);
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "MudLung_EN",
                    "MunglingMudLung_EN",
                ], null);
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Wringle_EN",
                    "Mung_EN",
                ], null);
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "TearDrinker_EN",
                    "Mung_EN",
                ], null);
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Acolyte_EN",
                    "Mung_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "ColophonDefeated_EN",
                ], null);
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "ColophonComposed_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Chiito_EN",
                ], null);
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Arceles_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "MudLung_EN",
                    "Madman_EN",
                ], null);
                acolyteMedium.CreateNewEnemyEncounterData(
                [
                    "Acolyte_EN",
                    "Acolyte_EN",
                    "Madman_EN",
                ], null);
            }
            acolyteMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Acolyte_Medium_EnemyBundle", 20, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 20
        }
    }
}
