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
            asterismMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Asterism_Medium_EnemyBundle", 8, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
